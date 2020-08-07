using Regulus.Remote;
using Regulus.Samples.Helloworld.Common;
using Regulus.Utility;
using System;
using System.Linq;
using System.Net;

namespace Regulus.Samples.Helloworld.Client
{   
    class Program
    {
        public static bool Enable = true;
        static void Main(string[] args)
        {
            var ip = IPAddress.Parse(args[0]);
            var port = int.Parse(args[1]);
            var protocolAsm = System.Reflection.Assembly.LoadFrom("Regulus.Samples.Helloworld.Protocol.dll");
            var protocol = Regulus.Remote.Protocol.ProtocolProvider.Create(protocolAsm);
            var agent = Regulus.Remote.Client.Provider.CreateAgent(protocol);
            var tcp = Regulus.Remote.Client.Provider.CreateTcp(agent);
            tcp.Connect(new IPEndPoint(ip, port)).Wait();
            agent.QueryNotifier<Common.IGreeter>().Supply += (greeter) => {
                String user = "you";
                greeter.SayHello(new HelloRequest() { Name = user}).OnValue += _GetReply;
            };
            while (Enable)
            {
                System.Threading.Thread.Sleep(0);
                agent.Update();
            }
            tcp.Disconnect().Wait();
            System.Console.WriteLine($"Press any key to end.");
            System.Console.ReadKey();
        }

        private static void _GetReply(HelloReply reply)
        {
            System.Console.WriteLine($"Receive message : {reply.Message}");
            Enable = false;
        }
    }
}

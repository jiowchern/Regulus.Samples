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
            var agent = Regulus.Remote.Client.AgentProvider.CreateTcp(protocolAsm) ;
            agent.Launch();
           
            agent.QueryNotifier<IConnect>().Supply += (connect) => {
                System.Console.WriteLine($"Connect to {ip}:{port} ... ");
                connect.Connect(new IPEndPoint(ip, port));
            };
            agent.QueryNotifier<Common.IEcho>().Supply += (echo)=> {
                System.Console.WriteLine($"Send message :Hello World!");
                echo.Speak("Hello World!").OnValue += _GetEcho;
            };
            while (Enable)
            {
                System.Threading.Thread.Sleep(0);
                agent.Update();
            }
            
            agent.Shutdown();
            System.Console.WriteLine($"Press any key to end.");
            System.Console.ReadKey();
        }

        private static void _GetEcho(string message)
        {
            System.Console.WriteLine($"Receive message : {message}");
            Enable = false;
        }
    }
}

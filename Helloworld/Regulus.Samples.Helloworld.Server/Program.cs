using System;
using System.Linq;
using System.Reflection;
using Regulus.Samples.Helloworld.Common;

namespace Regulus.Samples.Helloworld.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = int.Parse(args[0]);
            var protocolAsm = typeof(IGreeter).Assembly;
            var protocol = Regulus.Remote.Protocol.ProtocolProvider.Create(protocolAsm).FirstOrDefault();

            var entry = new Entry();

            var service = Regulus.Remote.Server.Provider.CreateService(entry, protocol);
            var listener = Regulus.Remote.Server.Provider.CreateTcp(service);
            
            listener.Bind(port);
            System.Console.WriteLine($"start.");
            while (entry.Enable)
            {
                System.Threading.Thread.Sleep(0);
            }
            listener.Close();
            service.Dispose();

            System.Console.WriteLine($"Press any key to end.");
            System.Console.ReadKey();
        }
    }
}

using System;
using System.Linq;
using System.Reflection;

namespace Regulus.Samples.Helloworld.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = int.Parse(args[0]);
            var protocolAsm = Assembly.LoadFrom("Regulus.Samples.Helloworld.Protocol.dll");
            var protocol = Regulus.Remote.Protocol.ProtocolProvider.Create(protocolAsm);

            var greeter = new Entry();
            var service = Regulus.Remote.Server.ServiceProvider.CreateTcp(greeter, port, protocol);
            service.Launch();
            while (greeter.Enable)
            {
                System.Threading.Thread.Sleep(0);
            }
            service.Shutdown();
            System.Console.WriteLine($"Press any key to end.");
            System.Console.ReadKey();
        }
    }
}

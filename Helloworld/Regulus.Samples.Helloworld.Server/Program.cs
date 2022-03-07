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
            
            var protocol = Regulus.Samples.Helloworld.Common.ProtocolCreater.Create();

            var entry = new Entry();

            var set = Regulus.Remote.Server.Provider.CreateTcpService(entry, protocol);
            var listener = set.Listener;
            var service = set.Service;


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

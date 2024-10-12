using Regulus.Utility.WindowConsoleAppliction;
using System.IO;
using System.Linq;
using Regulus.Samples.Chat1.Common;

namespace Regulus.Samples.Chat1.Client
{

    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="servicefile"></param>
        static void Main(System.IO.FileInfo servicefile )
        {
            if(servicefile != null)
            {
                System.Console.WriteLine("Standalone mode.");
                _RunStandalone( servicefile);
            }
            else
            {
                System.Console.WriteLine("Remote mode.");
                _RunRemote();
            }

        }

        private static void _RunStandalone( FileInfo servicefile)
        {
            var protocol = Regulus.Samples.Chat1.Common.ProtocolCreater.Create();
            var serviceAsm = System.Reflection.Assembly.LoadFrom(servicefile.FullName);
            var entrys = from type in serviceAsm.GetExportedTypes()
                         from interfaceType in type.GetInterfaces()
                         where interfaceType == typeof(Regulus.Remote.IEntry)
                         select System.Activator.CreateInstance(type) as Regulus.Remote.IEntry;
            var entry = entrys.Single();
            var service = Regulus.Remote.Standalone.Provider.CreateService(entry , protocol);



            var agent = service.Create();
            

            var console = new StandaloneConsole(agent);

            console.Run();

            service.Destroy(agent);            
            service.Dispose();

        }

        private static void _RunRemote()
        {
            var protocolAsm = typeof(IChatter).Assembly;
            var protocol = Regulus.Remote.Protocol.ProtocolProvider.Create(protocolAsm).First();
            
            var set = Regulus.Remote.Client.Provider.CreateTcpAgent(protocol);
            var console = new RemoteConsole(set.Connector , set.Agent);
            console.Run();
        }
    }
}

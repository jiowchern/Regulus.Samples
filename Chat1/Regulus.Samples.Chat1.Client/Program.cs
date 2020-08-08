using Regulus.Utility.WindowConsoleAppliction;
using System.IO;
using System.Linq;

namespace Regulus.Samples.Chat1.Client
{

    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="protocolfile"></param>
        /// <param name="servicefile"></param>
        static void Main(System.IO.FileInfo protocolfile , System.IO.FileInfo servicefile )
        {
            if(servicefile != null)
            {
                System.Console.WriteLine("Standalone mode.");
                _RunStandalone(protocolfile, servicefile);
            }
            else
            {
                System.Console.WriteLine("Remove mode.");
                _RunRemote(protocolfile);
            }

        }

        private static void _RunStandalone(FileInfo protocolfile, FileInfo servicefile)
        {
            var protocolAsm = System.Reflection.Assembly.LoadFrom(protocolfile.FullName);
            var protocol = Regulus.Remote.Protocol.ProtocolProvider.Create(protocolAsm);
            var serviceAsm = System.Reflection.Assembly.LoadFrom(servicefile.FullName);
            var entrys = from type in serviceAsm.GetExportedTypes()
                         from interfaceType in type.GetInterfaces()
                         where type.IsClass &&  interfaceType == typeof(Regulus.Remote.IEntry)
                         select System.Activator.CreateInstance(type) as Regulus.Remote.IEntry;
            var entry = entrys.Single();           
            var service = Regulus.Remote.Client.Provider.CreateService(protocol, entry);
            var agent = service.CreateNotifierQueryer();
            var console = new StandaloneConsole(service,agent);
            console.Run();
            service.DestroyNotifierQueryer(agent);
            service.Dispose();


        }

        private static void _RunRemote(FileInfo protocolfile)
        {
            /*var protocolAsm = System.Reflection.Assembly.LoadFrom(protocolfile.FullName);
            var agent = Regulus.Remote.Client.Provider.CreateTcp(protocolAsm);
            var console = new Console(agent);
            console.Run();*/
        }
    }
}

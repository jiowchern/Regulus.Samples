﻿using Regulus.Utility.WindowConsoleAppliction;
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
                System.Console.WriteLine("Remote mode.");
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
                         where interfaceType == typeof(Regulus.Remote.IEntry)
                         select System.Activator.CreateInstance(type) as Regulus.Remote.IEntry;
            var entry = entrys.Single();           
            var service = Regulus.Remote.Standalone.Provider.CreateService(protocol, entry);
            var agent = Regulus.Remote.Client.Provider.CreateAgent(protocol);
            service.Join(agent);            

            var console = new StandaloneConsole(agent);
            console.Run();
            service.Leave(agent);
            service.Dispose();


        }

        private static void _RunRemote(FileInfo protocolfile)
        {
            var protocolAsm = System.Reflection.Assembly.LoadFrom(protocolfile.FullName);
            var protocol = Regulus.Remote.Protocol.ProtocolProvider.Create(protocolAsm);
            var agent = Regulus.Remote.Client.Provider.CreateAgent(protocol);
            var connecter = Regulus.Remote.Client.Provider.CreateTcp(agent);
            var console = new RemoteConsole(connecter  , agent);
            console.Run();
        }
    }
}

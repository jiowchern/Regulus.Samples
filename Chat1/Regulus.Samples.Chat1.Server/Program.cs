using Regulus.Utility.WindowConsoleAppliction;
using System;

namespace Regulus.Samples.Chat1.Server
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="protocolfile"></param>
        /// <param name="port"></param>
        /// <param name="mode"></param>
        static void Main(System.IO.FileInfo protocolfile,int port,string mode)
        {
            var protocolAsm = System.Reflection.Assembly.LoadFrom(protocolfile.FullName);
            var protocol = Regulus.Remote.Protocol.ProtocolProvider.Create(protocolAsm);
            var room = new Regulus.Samples.Chat1.Service();
            var service = Regulus.Remote.Server.Provider.CreateService(room, protocol);
            if (mode.ToLower() == "tcp")
                _Tcp(port, service);
            if (mode.ToLower() == "websocket")
                _WebSocket(port, service);
        }

        private static void _WebSocket(int port, Remote.Soul.IService service)
        {
            var listener = Regulus.Remote.Server.Provider.CreateWebSocket(service);
            listener.Bind($"http://127.0.0.1:{port}/");
            var console = new Console();
            console.Run();
            listener.Close();
        }

        private static void _Tcp(int port, Remote.Soul.IService service)
        {
            
            var listener = Regulus.Remote.Server.Provider.CreateTcp(service);
            listener.Bind(port);
            var console = new Console();
            console.Run();
            listener.Close();
        }
    }
}

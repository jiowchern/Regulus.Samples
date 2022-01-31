using Regulus.Utility.WindowConsoleAppliction;
using System;
using Regulus.Samples.Chat1.Common;
using System.Linq;

namespace Regulus.Samples.Chat1.Server
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="port"></param>
        /// <param name="mode"></param>
        static void Main(int port,string mode)
        {
            
            var protocol = Regulus.Remote.Protocol.ProtocolProvider.Create(typeof(IChatter).Assembly).First();
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
            listener.Bind($"http://*:{port}/");
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

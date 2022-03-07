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
            
            var protocol = Regulus.Samples.Chat1.Common.ProtocolCreater.Create();
            var room = new Regulus.Samples.Chat1.Service();
            
            if (mode.ToLower() == "tcp")
                _Tcp(port, room, protocol);
            if (mode.ToLower() == "websocket")
                _WebSocket(port, room, protocol);
        }

        private static void _WebSocket(int port, Service room, Remote.IProtocol protocol)
        {
            var set = Regulus.Remote.Server.Provider.CreateWebService(room , protocol);
            var listener = set.Listener;
            listener.Bind($"http://*:{port}/");
            var console = new Console();
            console.Run();
            listener.Close();
            set.Service.Dispose();
        }


        private static void _Tcp(int port, Service room, Remote.IProtocol protocol)
        {

            var set = Regulus.Remote.Server.Provider.CreateTcpService(room, protocol);
            var listener = set.Listener;
            listener.Bind(port);
            var console = new Console();
            console.Run();
            listener.Close();
            set.Service.Dispose();
        }
    }
}

using Regulus.Remote;
using Regulus.Utility;

namespace Regulus.Samples.Chat1.Client
{
    public static class ConnectHelper
    {
        public static Regulus.Remote.Value<bool> Connect(this IConnect connect , string ip , int port)
        {
            return connect.Connect(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(ip), port));
        }
    }

    internal class ConnectStatus : IStatus
    {
        
        private readonly Command _Command;
        private readonly IConnect _Connect;



        public ConnectStatus(IConnect connect, Command command)
        {
            this._Connect = connect;
            this._Command = command;
        }

        void IStatus.Enter()
        {
            _Command.Register<string, int>("Connect", (ip, port) => _Connect.Connect(ip,port) );
        }

        void IStatus.Leave()
        {
            _Command.Unregister("Connect");
        }

        void IStatus.Update()
        {
            
        }

        
    }
}
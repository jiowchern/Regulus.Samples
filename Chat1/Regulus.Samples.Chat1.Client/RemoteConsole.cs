using Regulus.Network.Tcp;
using Regulus.Network.Web;
using Regulus.Remote.Client.Tcp;
using Regulus.Remote.Ghost;
using System.Net;

namespace Regulus.Samples.Chat1.Client
{
    class RemoteConsole : Console
    {
        private Connector _Connector ;
        private IAgent _Agent;

        public RemoteConsole(Connector connector , IAgent agent) : base(agent)
        {
            this._Connector  = connector ;
            this._Agent = agent;
        }
        protected override void _Shutdown()
        {
            _Agent.Disable();
            Command.Unregister("Connect");
            Command.Unregister("Disconnect");
            Command.Unregister("Ping");
        }
        protected override void _Launch()
        {
            Command.Register<string,int>("Connect" , _ConnectAsync);
            Command.Register("Ping", _Ping);
            
            base._Launch();
        }
        protected override void _Update()
        {
            _Agent.Update();
            base._Update();
        }

        private void _Ping()
        {
            System.Console.WriteLine($"ping:{_Agent.Ping}");
        }

        private void _ConnectAsync(string ip, int port)
        {
            var resultTask = _Connector .Connect(new IPEndPoint(IPAddress.Parse(ip) , port ));
            var peer = resultTask.Result;
            Command.Unregister("Disconnect");
            
            if (peer != null)
            {
                _Agent.Enable(peer);
                Command.Register("Disconnect", ()=> _Connector .Disconnect().Wait() );
            }
                

        }
    }
}

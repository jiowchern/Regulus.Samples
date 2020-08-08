using Regulus.Remote.Client.Tcp;
using Regulus.Remote.Ghost;
using System.Net;

namespace Regulus.Samples.Chat1.Client
{
    class RemoteConsole : Console
    {
        private Connecter _Connecter;
        private IAgent _Agent;

        public RemoteConsole(Connecter connecter, IAgent agent) : base(agent)
        {
            this._Connecter = connecter;
            this._Agent = agent;
        }
        protected override void _Shutdown()
        {
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
            var resultTask = _Connecter.Connect(new IPEndPoint(IPAddress.Parse(ip) , port ));
            var online = resultTask.Result;
            Command.Unregister("Disconnect");
            Command.Register("Disconnect", online.Disconnect);

        }
    }
}

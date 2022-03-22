using Regulus.Remote;
using System;
using System.Net;

namespace Regulus.Samples.Chat1.Bots
{
    class TcpApplication : Regulus.Utility.WindowConsole
    {

        readonly System.Collections.Generic.List<System.IDisposable> _Disposables;
        
        private IProtocol _Protocol;
        private int _BotCount;
        private IPEndPoint _IPEndPoint;

        public TcpApplication(IProtocol protocol, int botcount, IPEndPoint ipendpoint)
        {
            this._Protocol = protocol;
            this._BotCount = botcount;
            this._IPEndPoint = ipendpoint;
            _Disposables = new System.Collections.Generic.List<IDisposable>();
        }

        protected override void _Launch()
        {
            
        }

        protected override void _Shutdown()
        {
            lock(_Disposables)
            {
                foreach (var disposable in _Disposables)
                {
                    disposable.Dispose();
                }
            }
            
            lock(_Disposables)
                _Disposables.Clear();
        }

        protected override async void _Update()
        {
            while(_Disposables.Count < _BotCount)
            {
                var tcpSet = Regulus.Remote.Client.Provider.CreateTcpAgent(_Protocol);
                
                var agent = tcpSet.Agent;
                var tcp = tcpSet.Connecter;
                var result = await tcp.Connect(_IPEndPoint);
                if(result == false)
                {
                    return;
                }
                
                var bot = new Bot(agent);

                tcp.SocketErrorEvent += (e) =>
                {
                    lock (_Disposables)
                    {
                        _Disposables.Remove(bot);
                    }
                };
                lock (_Disposables)
                    _Disposables.Add(bot);
            }
        }
    }
}

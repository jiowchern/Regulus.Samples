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
                
                var agent = Regulus.Remote.Client.Provider.CreateAgent(_Protocol);
                var tcp = Regulus.Remote.Client.Provider.CreateTcp(agent);
                var online = await tcp.Connect(_IPEndPoint);
                if(online == null)
                {
                    return;
                }
                
                var bot = new Bot(agent);
                online.ErrorEvent += (e) =>
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

using Regulus.Remote;
using System;

namespace Regulus.Samples.Chat1.Bots
{
    class StandaloneApplication : Regulus.Utility.WindowConsole
    {
        private readonly IProtocol _Protocol;
        private readonly int _BotCount;
        private readonly System.Collections.Generic.List<IDisposable> _Disposables;
        private readonly Service _Game;
        private readonly Remote.Standalone.IService _Service;

        public StandaloneApplication(IProtocol protocol, int botcount)
        {
            this._Protocol = protocol;
            this._BotCount = botcount;
            _Disposables = new System.Collections.Generic.List<IDisposable>();
            _Game = new Regulus.Samples.Chat1.Service();
            _Service = Regulus.Remote.Standalone.Provider.CreateService(protocol, _Game);
        }
        protected override void _Launch()
        {
            
        }

        protected override void _Shutdown()
        {
            lock (_Disposables)
            {
                foreach (var disposable in _Disposables)
                {                    
                    disposable.Dispose();
                }
            }

            lock (_Disposables)
                _Disposables.Clear();

            _Service.Dispose();
        }

        protected override void _Update()
        {
            while (_Disposables.Count < _BotCount)
            {

                var agent = Regulus.Remote.Client.Provider.CreateAgent(_Protocol);


                _Service.Join(agent);

                var bot = new Bot(agent);
                
                lock (_Disposables)
                    _Disposables.Add(bot);
            }
        }
    }
}

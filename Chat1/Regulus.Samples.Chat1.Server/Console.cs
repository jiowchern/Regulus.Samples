using Regulus.Remote.Soul;
namespace Regulus.Samples.Chat1.Server
{
    class Console : Regulus.Utility.WindowConsole
    {
        private Remote.Soul.Service _Service;

        public Console(Remote.Soul.Service service)
        {
            this._Service = service;
        }

        protected override void _Launch()
        {
            _Service.Launch();
        }

        protected override void _Shutdown()
        {
            _Service.Shutdown();
        }

        protected override void _Update()
        {
            
        }
    }
}

using Regulus.Remote;

namespace Regulus.Samples.Chat1.Client
{
    class StandaloneConsole : Console
    {
        readonly Regulus.Remote.Standalone.IService _Service;
        public StandaloneConsole(Regulus.Remote.Standalone.IService service, INotifierQueryable notifierQueryable) : base(notifierQueryable)
        {
            _Service = service;
        }
        protected override void _Update()
        {
            _Service.Update();

            base._Update();
        }
    }
}

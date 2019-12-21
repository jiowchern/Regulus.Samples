using Regulus.Remote.Soul;

namespace Regulus.Samples.Chat1.Server
{
    class Application : Regulus.Utility.WindowConsole
    {
        private readonly Service _Service;

        public Application(int port)
        {
            var commonAssembly = System.Reflection.Assembly.LoadFile(System.IO.Path.GetFullPath("Regulus.Samples.Chat1.Common.dll"));
            _Service = Regulus.Remote.Server.ServiceProvider.CreateTcp(port, new Room(), Remote.Protocol.Essential.CreateFromDomain(commonAssembly));            
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


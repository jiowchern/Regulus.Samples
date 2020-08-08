using Regulus.Remote;
using Regulus.Remote.Ghost;
using Regulus.Remote.Standalone;
using Regulus.Samples.Chat1.Common;
using System;

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
    class Console : Regulus.Utility.WindowConsole
    {
        
        readonly Regulus.Utility.StatusMachine _Machine;
        readonly Remote.INotifierQueryable _Agent;

        public Console(Remote.INotifierQueryable agent)
        {            
            _Machine = new Utility.StatusMachine();
            _Agent = agent;
        }

        protected override void _Launch()
        {

            _Agent.QueryNotifier<ILogin>().Supply += _ToLogin;
            _Agent.QueryNotifier<IPlayer>().Supply += _ToChat;
            
        }
        protected override void _Shutdown()
        {
            _Agent.QueryNotifier<ILogin>().Supply -= _ToLogin;
            _Agent.QueryNotifier<IPlayer>().Supply -= _ToChat;
            
            _Machine.Termination();            
        }

        private void _ToLogin(ILogin login)
        {
            var status = new Regulus.Samples.Chat1.Client.LoginStatus(login, Command);
            _Machine.Push(status);
        }
        private void _ToChat(IPlayer player)
        {
            var status = new ChatroomStatus(player, Command);

            _Machine.Push(status);
        }

        protected override void _Update()
        {
            
            _Machine.Update();
        }
    }
}

using Regulus.Remote;
using System;

namespace Regulus.Samples.Chat1.Client
{
    class Console : Regulus.Utility.WindowConsole
    {
        private readonly IAgent _Agent;
        readonly Regulus.Utility.StatusMachine _Machine;
        public Console(IAgent agent)
        {
            this._Agent = agent;
            _Machine = new Utility.StatusMachine();
        }

        protected override void _Launch()
        {
            _Agent.Launch();
            _Agent.QueryNotifier<IConnect>().Supply += _ToConnect;
            _Agent.QueryNotifier<IOnline>().Supply += _ToOnline;
        }
        protected override void _Shutdown()
        {
            _Agent.QueryNotifier<IOnline>().Supply -= _ToOnline;
            _Agent.QueryNotifier<IConnect>().Supply -= _ToConnect;
            _Machine.Termination();
            _Agent.Shutdown();
        }

        private void _ToConnect(IConnect connect)
        {
            Utility.IStatus status = new ConnectStatus(connect, Command);
            _Machine.Push(status);
        }

        private void _ToOnline(IOnline online)
        {
            Utility.IStatus status = new OnlineStatus(_Agent, online, Command);
            _Machine.Push(status);
        }

        protected override void _Update()
        {
            _Agent.Update();
            _Machine.Update();
        }
    }
}

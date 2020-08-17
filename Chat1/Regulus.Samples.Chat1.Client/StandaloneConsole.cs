using Regulus.Remote;
using Regulus.Remote.Ghost;

namespace Regulus.Samples.Chat1.Client
{
    class StandaloneConsole : Console
    {
        private readonly IAgent _Agent;

        public StandaloneConsole(IAgent agent) : base(agent)
        {
            this._Agent = agent;
        }
        protected override void _Update()
        {
            _Agent.Update();
            base._Update();
        }
    }
}

                    

namespace Regulus.Samples.Chat1.Common.Adsorption
{
    using System.Linq;
        
    public class BroadcastableAdsorber : UnityEngine.MonoBehaviour , Regulus.Remote.Unity.Adsorber<IBroadcastable>
    {
        private readonly Regulus.Utility.StageMachine _Machine;        
        
        public string Agent;

        private global::Regulus.Samples.Chat1Agent _Agent;

        [System.Serializable]
        public class UnityEnableEvent : UnityEngine.Events.UnityEvent<bool> {}
        public UnityEnableEvent EnableEvent;
        [System.Serializable]
        public class UnitySupplyEvent : UnityEngine.Events.UnityEvent<Regulus.Samples.Chat1.Common.IBroadcastable> {}
        public UnitySupplyEvent SupplyEvent;
        Regulus.Samples.Chat1.Common.IBroadcastable _Broadcastable;                        
       
        public BroadcastableAdsorber()
        {
            _Machine = new Regulus.Utility.StageMachine();
        }

        void Start()
        {
            _Machine.Push(new Regulus.Utility.SimpleStage(_ScanEnter, _ScanLeave, _ScanUpdate));
        }

        private void _ScanUpdate()
        {
            var agents = UnityEngine.GameObject.FindObjectsOfType<global::Regulus.Samples.Chat1Agent>();
            _Agent = agents.FirstOrDefault(d => string.IsNullOrEmpty(d.Name) == false && d.Name == Agent);
            if(_Agent != null)
            {
                _Machine.Push(new Regulus.Utility.SimpleStage(_DispatchEnter, _DispatchLeave));
            }            
        }

        private void _DispatchEnter()
        {
            _Agent.Distributor.Attach<IBroadcastable>(this);
        }

        private void _DispatchLeave()
        {
            _Agent.Distributor.Detach<IBroadcastable>(this);
        }

        private void _ScanLeave()
        {

        }


        private void _ScanEnter()
        {

        }

        void Update()
        {
            _Machine.Update();
        }

        void OnDestroy()
        {
            _Machine.Termination();
        }

        public Regulus.Samples.Chat1.Common.IBroadcastable GetGPI()
        {
            return _Broadcastable;
        }
        public void Supply(Regulus.Samples.Chat1.Common.IBroadcastable gpi)
        {
            _Broadcastable = gpi;
            _Broadcastable.MessageEvent += _OnMessageEvent;
            EnableEvent.Invoke(true);
            SupplyEvent.Invoke(gpi);
        }

        public void Unsupply(Regulus.Samples.Chat1.Common.IBroadcastable gpi)
        {
            EnableEvent.Invoke(false);
            _Broadcastable.MessageEvent -= _OnMessageEvent;
            _Broadcastable = null;
        }
        
        
        
        [System.Serializable]
        public class UnityMessageEvent : UnityEngine.Events.UnityEvent<System.String,System.String> { }
        public UnityMessageEvent MessageEvent;
        
        
        private void _OnMessageEvent(System.String arg0,System.String arg1)
        {
            MessageEvent.Invoke(arg0,arg1);
        }
        
    }
}
                    
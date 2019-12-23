                    

namespace Regulus.Samples.Chat1.Common.Adsorption
{
    using System.Linq;
        
    public class ChatableAdsorber : UnityEngine.MonoBehaviour , Regulus.Remote.Unity.Adsorber<IChatable>
    {
        private readonly Regulus.Utility.StageMachine _Machine;        
        
        public string Agent;

        private global::Regulus.Samples.Chat1Agent _Agent;

        [System.Serializable]
        public class UnityEnableEvent : UnityEngine.Events.UnityEvent<bool> {}
        public UnityEnableEvent EnableEvent;
        [System.Serializable]
        public class UnitySupplyEvent : UnityEngine.Events.UnityEvent<Regulus.Samples.Chat1.Common.IChatable> {}
        public UnitySupplyEvent SupplyEvent;
        Regulus.Samples.Chat1.Common.IChatable _Chatable;                        
       
        public ChatableAdsorber()
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
            _Agent.Distributor.Attach<IChatable>(this);
        }

        private void _DispatchLeave()
        {
            _Agent.Distributor.Detach<IChatable>(this);
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

        public Regulus.Samples.Chat1.Common.IChatable GetGPI()
        {
            return _Chatable;
        }
        public void Supply(Regulus.Samples.Chat1.Common.IChatable gpi)
        {
            _Chatable = gpi;
            
            EnableEvent.Invoke(true);
            SupplyEvent.Invoke(gpi);
        }

        public void Unsupply(Regulus.Samples.Chat1.Common.IChatable gpi)
        {
            EnableEvent.Invoke(false);
            
            _Chatable = null;
        }
        
        public void Send(System.String name,System.String message)
        {
            if(_Chatable != null)
            {
                _Chatable.Send(name,message);
            }
        }
        
        
    }
}
                    
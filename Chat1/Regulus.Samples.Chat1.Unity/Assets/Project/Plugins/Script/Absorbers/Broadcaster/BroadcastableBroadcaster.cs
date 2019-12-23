
using System;

using System.Linq;

using Regulus.Utility;

using UnityEngine;
using UnityEngine.Events;

namespace Regulus.Samples{ 
    public class BroadcastableBroadcaster : UnityEngine.MonoBehaviour 
    {
        public string Agent;        
        Regulus.Remote.INotifier<Regulus.Samples.Chat1.Common.IBroadcastable> _Notifier;

        private readonly Regulus.Utility.StageMachine _Machine;

        public BroadcastableBroadcaster()
        {
            _Machine = new StageMachine();
        } 

        void Start()
        {
            _ToScan();
        }

        private void _ToScan()
        {
            var stage = new Regulus.Utility.SimpleStage(_ScanEnter , _ScaneLeave , _ScaneUpdate);

            _Machine.Push(stage);
        }


        private void _ScaneUpdate()
        {
            var agents = GameObject.FindObjectsOfType<Regulus.Samples.Chat1Agent>();
            var agent = agents.FirstOrDefault(d => d.Name == Agent);
            if (agent != null)
            {
                _Notifier = agent.Distributor.QueryNotifier<Regulus.Samples.Chat1.Common.IBroadcastable>();

                _ToInitial();                                
            }
        }

        private void _ToInitial()
        {
            var stage = new Regulus.Utility.SimpleStage(_Initial);
            _Machine.Push(stage);
        }

        private void _Initial()
        {
            _Notifier.Supply += _Supply;
            _Notifier.Unsupply += _Unsupply;
        }

        private void _ScaneLeave()
        {
            
        }

        private void _ScanEnter()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            _Machine.Update();
        }

        void OnDestroy()
        {
            if (_Notifier != null)
            {
                _Notifier.Supply -= _Supply;
                _Notifier.Unsupply -= _Unsupply;
            }
                
            _Machine.Termination();
        }

        private void _Unsupply(Regulus.Samples.Chat1.Common.IBroadcastable obj)
        {
            UnsupplyEvent.Invoke(obj);
        }

        private void _Supply(Regulus.Samples.Chat1.Common.IBroadcastable obj)
        {
            SupplyEvent.Invoke(obj);
        }

        [Serializable]
        public class UnityBroadcastEvent : UnityEvent<Regulus.Samples.Chat1.Common.IBroadcastable>{}

        public UnityBroadcastEvent SupplyEvent;
        public UnityBroadcastEvent UnsupplyEvent;
    }
}

   
    using System;  
    
    using System.Collections.Generic;
    
    namespace Regulus.Samples.Chat1.Common.Ghost 
    { 
        public class CIBroadcastable : Regulus.Samples.Chat1.Common.IBroadcastable , Regulus.Remote.IGhost
        {
            readonly bool _HaveReturn ;
            
            readonly long _GhostIdName;
            
            
            
            public CIBroadcastable(long id, bool have_return )
            {
                _HaveReturn = have_return ;
                _GhostIdName = id;            
            }
            

            long Regulus.Remote.IGhost.GetID()
            {
                return _GhostIdName;
            }

            bool Regulus.Remote.IGhost.IsReturnType()
            {
                return _HaveReturn;
            }
            object Regulus.Remote.IGhost.GetInstance()
            {
                return this;
            }

            private event Regulus.Remote.CallMethodCallback _CallMethodEvent;

            event Regulus.Remote.CallMethodCallback Regulus.Remote.IGhost.CallMethodEvent
            {
                add { this._CallMethodEvent += value; }
                remove { this._CallMethodEvent -= value; }
            }

            private event Regulus.Remote.EventNotifyCallback _AddEventEvent;

            event Regulus.Remote.EventNotifyCallback Regulus.Remote.IGhost.AddEventEvent
            {
                add { this._AddEventEvent += value; }
                remove { this._AddEventEvent -= value; }
            }

            private event Regulus.Remote.EventNotifyCallback _RemoveEventEvent;

            event Regulus.Remote.EventNotifyCallback Regulus.Remote.IGhost.RemoveEventEvent
            {
                add { this._RemoveEventEvent += value; }
                remove { this._RemoveEventEvent -= value; }
            }
            
            


                public Regulus.Remote.GhostEventHandler _MessageEvent = new Regulus.Remote.GhostEventHandler();
                event System.Action<System.String,System.String> Regulus.Samples.Chat1.Common.IBroadcastable.MessageEvent
                {
                    add { 
                            var id = _MessageEvent.Add(value);
                            _AddEventEvent(typeof(Regulus.Samples.Chat1.Common.IBroadcastable).GetEvent("MessageEvent"),id);
                        }
                    remove { 
                                var id = _MessageEvent.Remove(value);
                                _RemoveEventEvent(typeof(Regulus.Samples.Chat1.Common.IBroadcastable).GetEvent("MessageEvent"),id);
                            }
                }
                
            
        }
    }

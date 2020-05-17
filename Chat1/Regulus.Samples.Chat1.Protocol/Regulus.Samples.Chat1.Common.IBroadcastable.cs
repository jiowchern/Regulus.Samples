   
    using System;  
    
    using System.Collections.Generic;
    
    namespace Regulus.Samples.Chat1.Common.Ghost 
    { 
        public class CIBroadcastable : Regulus.Samples.Chat1.Common.IBroadcastable , Regulus.Remote.IGhost
        {
            readonly bool _HaveReturn ;
            
            readonly Guid _GhostIdName;
            
            
            
            public CIBroadcastable(Guid id, bool have_return )
            {
                _HaveReturn = have_return ;
                _GhostIdName = id;            
            }
            

            Guid Regulus.Remote.IGhost.GetID()
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
            
            


                public System.Action<System.String,System.String> _MessageEvent;
                event System.Action<System.String,System.String> Regulus.Samples.Chat1.Common.IBroadcastable.MessageEvent
                {
                    add { _MessageEvent += value;}
                    remove { _MessageEvent -= value;}
                }
                
            
        }
    }

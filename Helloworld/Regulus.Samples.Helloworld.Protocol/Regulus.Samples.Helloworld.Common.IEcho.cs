   
    using System;  
    
    using System.Collections.Generic;
    
    namespace Regulus.Samples.Helloworld.Common.Ghost 
    { 
        public class CIEcho : Regulus.Samples.Helloworld.Common.IEcho , Regulus.Remote.IGhost
        {
            readonly bool _HaveReturn ;
            
            readonly Guid _GhostIdName;
            
            
            
            public CIEcho(Guid id, bool have_return )
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
            
            
                Regulus.Remote.Value<System.String> Regulus.Samples.Helloworld.Common.IEcho.Speak(System.String _1)
                {                    

                    
    var returnValue = new Regulus.Remote.Value<System.String>();
    

                    var info = typeof(Regulus.Samples.Helloworld.Common.IEcho).GetMethod("Speak");
                    _CallMethodEvent(info , new object[] {_1} , returnValue);                    
                    return returnValue;
                }

                



            
        }
    }

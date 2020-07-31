   
    using System;  
    
    using System.Collections.Generic;
    
    namespace Regulus.Samples.Chat1.Common.Ghost 
    { 
        public class CIChatter : Regulus.Samples.Chat1.Common.IChatter , Regulus.Remote.IGhost
        {
            readonly bool _HaveReturn ;
            
            readonly long _GhostIdName;
            
            
            
            public CIChatter(long id, bool have_return )
            {
                // notifier propertys
                //Name
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
            event Regulus.Remote.PropertyNotifierCallback _AddSupplyNoitfierEvent;
            event Regulus.Remote.PropertyNotifierCallback Regulus.Remote.IGhost.AddSupplyNoitfierEvent
            {

                add
                {
                    _AddSupplyNoitfierEvent += value;
                }

                remove
                {
                    _AddSupplyNoitfierEvent -= value;
                }
            }

            event Regulus.Remote.PropertyNotifierCallback _RemoveSupplyNoitfierEvent;
            event Regulus.Remote.PropertyNotifierCallback Regulus.Remote.IGhost.RemoveSupplyNoitfierEvent
            {
                add
                {
                    _RemoveSupplyNoitfierEvent += value;
                }

                remove
                {
                    _RemoveSupplyNoitfierEvent -= value;
                }
            }

            event Regulus.Remote.PropertyNotifierCallback _AddUnsupplyNoitfierEvent;
            event Regulus.Remote.PropertyNotifierCallback Regulus.Remote.IGhost.AddUnsupplyNoitfierEvent
            {
                add
                {
                    _AddUnsupplyNoitfierEvent += value;
                }

                remove
                {
                    _AddUnsupplyNoitfierEvent -= value;
                }
            }

            event Regulus.Remote.PropertyNotifierCallback _RemoveUnsupplyNoitfierEvent;
            event Regulus.Remote.PropertyNotifierCallback Regulus.Remote.IGhost.RemoveUnsupplyNoitfierEvent
            {
                add
                {
                    _RemoveUnsupplyNoitfierEvent += value;
                }

                remove
                {
                    _RemoveUnsupplyNoitfierEvent -= value;
                }
            }
            
                void Regulus.Samples.Chat1.Common.IChatter.Whisper(System.String _1)
                {                    

                    Regulus.Remote.IValue returnValue = null;
                    var info = typeof(Regulus.Samples.Chat1.Common.IChatter).GetMethod("Whisper");
                    _CallMethodEvent(info , new object[] {_1} , returnValue);                    
                    
                }

                


                public Regulus.Remote.Property<System.String> _Name= new Regulus.Remote.Property<System.String>();
                Regulus.Remote.Property<System.String> Regulus.Samples.Chat1.Common.IChatter.Name { get{ return _Name;} }

            
        }
    }

   
    using System;  
    
    using System.Collections.Generic;
    
    namespace Regulus.Samples.Chat1.Common.Ghost 
    { 
        public class CIPlayer : Regulus.Samples.Chat1.Common.IPlayer , Regulus.Remote.IGhost
        {
            readonly bool _HaveReturn ;
            
            readonly long _GhostIdName;
            
            
            
            public CIPlayer(long id, bool have_return )
            {
                // notifier propertys
                //Chatters
_Chatters = new Regulus.Remote.GhostNotifier<Regulus.Samples.Chat1.Common.IChatter>((p) => _AddSupplyNoitfierEvent(typeof(IPlayer).GetProperty("Chatters"), p), (p) => _RemoveSupplyNoitfierEvent(typeof(IPlayer).GetProperty("Chatters"),p), (p) => _AddUnsupplyNoitfierEvent(typeof(IPlayer).GetProperty("Chatters"), p), (p) => _RemoveUnsupplyNoitfierEvent(typeof(IPlayer).GetProperty("Chatters"),p));
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
            
                void Regulus.Samples.Chat1.Common.IPlayer.Send(System.String _1)
                {                    

                    Regulus.Remote.IValue returnValue = null;
                    var info = typeof(Regulus.Samples.Chat1.Common.IPlayer).GetMethod("Send");
                    _CallMethodEvent(info , new object[] {_1} , returnValue);                    
                    
                }

                
 

                void Regulus.Samples.Chat1.Common.IPlayer.Quit()
                {                    

                    Regulus.Remote.IValue returnValue = null;
                    var info = typeof(Regulus.Samples.Chat1.Common.IPlayer).GetMethod("Quit");
                    _CallMethodEvent(info , new object[] {} , returnValue);                    
                    
                }

                


            readonly Regulus.Remote.GhostNotifier<Regulus.Samples.Chat1.Common.IChatter> _Chatters;
            Regulus.Remote.INotifier<Regulus.Samples.Chat1.Common.IChatter> Regulus.Samples.Chat1.Common.IPlayer.Chatters { get{ return _Chatters;} }

                public Regulus.Remote.GhostEventHandler _PublicMessageEvent = new Regulus.Remote.GhostEventHandler();
                event System.Action<System.String,System.String> Regulus.Samples.Chat1.Common.IPlayer.PublicMessageEvent
                {
                    add { 
                            var id = _PublicMessageEvent.Add(value);
                            _AddEventEvent(typeof(Regulus.Samples.Chat1.Common.IPlayer).GetEvent("PublicMessageEvent"),id);
                        }
                    remove { 
                                var id = _PublicMessageEvent.Remove(value);
                                _RemoveEventEvent(typeof(Regulus.Samples.Chat1.Common.IPlayer).GetEvent("PublicMessageEvent"),id);
                            }
                }
                

                public Regulus.Remote.GhostEventHandler _PrivateMessageEvent = new Regulus.Remote.GhostEventHandler();
                event System.Action<System.String,System.String> Regulus.Samples.Chat1.Common.IPlayer.PrivateMessageEvent
                {
                    add { 
                            var id = _PrivateMessageEvent.Add(value);
                            _AddEventEvent(typeof(Regulus.Samples.Chat1.Common.IPlayer).GetEvent("PrivateMessageEvent"),id);
                        }
                    remove { 
                                var id = _PrivateMessageEvent.Remove(value);
                                _RemoveEventEvent(typeof(Regulus.Samples.Chat1.Common.IPlayer).GetEvent("PrivateMessageEvent"),id);
                            }
                }
                
            
        }
    }

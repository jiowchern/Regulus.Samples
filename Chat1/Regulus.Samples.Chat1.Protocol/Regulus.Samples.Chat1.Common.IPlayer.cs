   
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

                


                    public Regulus.Remote.Notifier<Regulus.Samples.Chat1.Common.IChatter> _Chatters= new Regulus.Remote.Notifier<Regulus.Samples.Chat1.Common.IChatter>();
                    Regulus.Remote.Notifier<Regulus.Samples.Chat1.Common.IChatter> Regulus.Samples.Chat1.Common.IPlayer.Chatters { get{ return _Chatters;} }

                public Regulus.Remote.GhostEventHandler _PublicMessageEvent = new Regulus.Remote.GhostEventHandler();
                event System.Action<Regulus.Samples.Chat1.Common.Message> Regulus.Samples.Chat1.Common.IPlayer.PublicMessageEvent
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
                event System.Action<Regulus.Samples.Chat1.Common.Message> Regulus.Samples.Chat1.Common.IPlayer.PrivateMessageEvent
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

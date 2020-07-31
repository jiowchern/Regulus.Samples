
    using System;  
    using System.Collections.Generic;
    
    namespace Regulus.Samples.Chat1.Common.Invoker.IPlayer 
    { 
        public class PrivateMessageEvent : Regulus.Remote.IEventProxyCreator
        {

            Type _Type;
            string _Name;
            
            public PrivateMessageEvent()
            {
                _Name = "PrivateMessageEvent";
                _Type = typeof(Regulus.Samples.Chat1.Common.IPlayer);                   
            
            }
            Delegate Regulus.Remote.IEventProxyCreator.Create(long soul_id,int event_id,long handler_id, Regulus.Remote.InvokeEventCallabck invoke_Event)
            {                
                var closure = new Regulus.Remote.GenericEventClosure<System.String,System.String>(soul_id , event_id ,handler_id, invoke_Event);                
                return new Action<System.String,System.String>(closure.Run);
            }
        

            Type Regulus.Remote.IEventProxyCreator.GetType()
            {
                return _Type;
            }            

            string Regulus.Remote.IEventProxyCreator.GetName()
            {
                return _Name;
            }            
        }
    }
                
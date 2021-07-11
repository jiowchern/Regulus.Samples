
    using System;  
    using System.Collections.Generic;
    
    namespace Regulus.Samples.Chat1.Common.Invoker.IPlayer 
    { 
        public class PublicMessageEvent : Regulus.Remote.IEventProxyCreator
        {

            Type _Type;
            string _Name;
            
            public PublicMessageEvent()
            {
                _Name = "PublicMessageEvent";
                _Type = typeof(Regulus.Samples.Chat1.Common.IPlayer);                   
            
            }
            Delegate Regulus.Remote.IEventProxyCreator.Create(long soul_id,int event_id,long handler_id, Regulus.Remote.InvokeEventCallabck invoke_Event)
            {                
                var closure = new Regulus.Remote.GenericEventClosure<Regulus.Samples.Chat1.Common.Message>(soul_id , event_id ,handler_id, invoke_Event);                
                return new Action<Regulus.Samples.Chat1.Common.Message>(closure.Run);
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
                
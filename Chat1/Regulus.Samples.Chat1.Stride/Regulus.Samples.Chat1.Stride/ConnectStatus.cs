using Regulus.Network.Tcp;
using Regulus.Utility;
using System.Net;
using System.Runtime.CompilerServices;

namespace Regulus.Samples.Chat1.Stride
{
    internal class ConnectStatus : Regulus.Utility.IStatus
    {
        
        private readonly TaskAwaiter<bool> _Waiter;



        public event System.Action SuccessEvent;
        public event System.Action FailEvent;
        public ConnectStatus(TaskAwaiter<bool> awaiter)
        {            

            _Waiter = awaiter;
        }

        void IStatus.Enter()
        {
            
        }

        void IStatus.Leave()
        {
            
        }

        void IStatus.Update()
        {
            if(_Waiter.IsCompleted)
            {
                if(_Waiter.GetResult())
                {
                    SuccessEvent();
                }
                else
                {
                    FailEvent();
                }
            }
        }
    }
}
using Regulus.Framework;
using Regulus.Remote;
using Regulus.Samples.Helloworld.Common;
using System;

namespace Regulus.Samples.Helloworld.Server
{
    internal class Echo : Regulus.Remote.IEntry , IEcho
    {

        public volatile bool  Enable;
        public Echo()
        {
            Enable = true;
        }

        void IBinderProvider.AssignBinder(IBinder binder)
        {
            binder.BreakEvent += _End;
            binder.Bind<IEcho>(this);            
        }

        private void _End()
        {
            Enable = false;
        }

        void IBootable.Launch()
        {
            Console.WriteLine("Server launch.");
        }

        void IBootable.Shutdown()
        {
            Console.WriteLine("Server shutdown.");
        }

        Value<string> IEcho.Speak(string message)
        {
            Console.WriteLine($"Server receive message :{message}.");
            return message;
        }
    }
}
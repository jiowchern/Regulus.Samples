using Regulus.Framework;
using Regulus.Remote;
using Regulus.Samples.Helloworld.Common;
using System;

namespace Regulus.Samples.Helloworld.Server
{
    internal class Greeter : Regulus.Remote.IEntry , IGreeter
    {

        public volatile bool  Enable;
        public Greeter()
        {
            Enable = true;
        }

        void IBinderProvider.AssignBinder(IBinder binder)
        {
            binder.BreakEvent += _End;
            binder.Bind<IGreeter>(this);
            // unbind : binder.Unbind<IGreeter>(this);

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

        

        Value<HelloReply> IGreeter.SayHello(HelloRequest request)
        {
            return new HelloReply() { Message = $"Hello {request.Name}." };
        }
    }
}
using System;

namespace Regulus.Samples.Helloworld.Common
{
    public struct HelloRequest
    {
        public string Name;
    }

    public struct HelloReply
    {
        public string Message;
    }
    public interface IGreeter
    {
        Regulus.Remote.Value<HelloReply> SayHello(HelloRequest request);
    }
}

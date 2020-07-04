using System;

namespace Regulus.Samples.Helloworld.Common
{
    public class HelloRequest
    {
        public string Name;
    }

    public class HelloReply
    {
        public string Message;
    }
    public interface IGreeter
    {
        Regulus.Remote.Value<HelloReply> SayHello(HelloRequest request);
    }
}

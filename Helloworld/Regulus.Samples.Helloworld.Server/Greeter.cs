using Regulus.Remote;
using Regulus.Samples.Helloworld.Common;

namespace Regulus.Samples.Helloworld.Server
{
    class Greeter : IGreeter
    {
        Value<HelloReply> IGreeter.SayHello(HelloRequest request)
        {
            return new HelloReply() { Message = $"Hello {request.Name}." };
        }
    }
}
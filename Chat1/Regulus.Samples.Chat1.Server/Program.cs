using System;

namespace Regulus.Samples.Chat1.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = Regulus.Remote.Server.ServiceProvider.CreateTcp(int.Parse(args[0]), new Room(), Remote.Protocol.Essential.CreateFromDomain());
            service.Launch();
            

        }
    }
}


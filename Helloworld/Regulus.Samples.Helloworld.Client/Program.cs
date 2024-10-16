﻿using Regulus.Remote;
using Regulus.Samples.Helloworld.Common;
using Regulus.Utility;
using System;
using System.Linq;
using System.Net;

namespace Regulus.Samples.Helloworld.Client
{   
    class Program
    {
        public static bool Enable = true;
        static void Main(string[] args)
        {
            var ip = IPAddress.Parse(args[0]);
            var port = int.Parse(args[1]);
            var protocolAsm = typeof(IGreeter).Assembly;
            var protocol = Regulus.Samples.Helloworld.Common.ProtocolCreater.Create();
            var set = Regulus.Remote.Client.Provider.CreateTcpAgent(protocol);
            var tcp = set.Connecter;
            var agent = set.Agent;
            var connectTask = tcp.Connect(new IPEndPoint(ip, port));
            connectTask.Wait();
            var online = connectTask.Result;
            agent.QueryNotifier<Common.IGreeter>().Supply += (greeter) => {
                String user = "you";
                greeter.SayHello(new HelloRequest() { Name = user}).OnValue += _GetReply;
            };

            while (Enable)
            {
                System.Threading.Thread.Sleep(0);
                agent.Update();
            }

            tcp.Disconnect();
            agent.Dispose();
            System.Console.WriteLine($"Press any key to end.");
            System.Console.ReadKey();
        }

        private static void _GetReply(HelloReply reply)
        {
            System.Console.WriteLine($"Receive message : {reply.Message}");
            Enable = false;
        }
    }
}

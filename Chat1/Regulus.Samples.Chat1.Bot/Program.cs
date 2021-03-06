﻿using Regulus.Utility.WindowConsoleAppliction;
namespace Regulus.Samples.Chat1.Bots
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="protocolfile"></param>
        /// <param name="address"></param>
        /// <param name="port"></param>
        /// <param name="botcount"></param>
        /// <param name="mode"></param>
        static void Main(System.IO.FileInfo protocolfile,string address,int port,int botcount,string mode)
        {
            var protocolAsm = System.Reflection.Assembly.LoadFrom(protocolfile.FullName);
            var protocol = Regulus.Remote.Protocol.ProtocolProvider.Create(protocolAsm);

            if(mode == "tcp")
            {
                var app = new TcpApplication(protocol, botcount, new System.Net.IPEndPoint(System.Net.IPAddress.Parse(address), port));
                app.Run();
            }
            else if (mode == "standalone")
            {
                var app = new StandaloneApplication(protocol, botcount);
                app.Run();
            }
            

            

        }
    }
}

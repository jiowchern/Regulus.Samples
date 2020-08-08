using Regulus.Utility.WindowConsoleAppliction;
namespace Regulus.Samples.Chat1.Server
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="protocolfile"></param>
        /// <param name="port"></param>
        static void Main(System.IO.FileInfo protocolfile,int port)
        {
            var protocolAsm = System.Reflection.Assembly.LoadFrom(protocolfile.FullName);
            var protocol = Regulus.Remote.Protocol.ProtocolProvider.Create(protocolAsm);
            var room = new Regulus.Samples.Chat1.Service();
            var listener = Regulus.Remote.Server.Provider.CreateTcp(Regulus.Remote.Server.Provider.CreateService(room , protocol));
            listener.Bind(port);
            var console = new Console();            
            console.Run();
            listener.Close();
        }
    }
}

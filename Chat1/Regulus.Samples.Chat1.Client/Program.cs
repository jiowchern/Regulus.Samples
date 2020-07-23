using Regulus.Utility.WindowConsoleAppliction;

namespace Regulus.Samples.Chat1.Client
{

    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="protocolfile"></param>
        static void Main(System.IO.FileInfo protocolfile)
        {
            var protocolAsm = System.Reflection.Assembly.LoadFrom(protocolfile.FullName);
            var agent = Regulus.Remote.Client.AgentProvider.CreateTcp(protocolAsm);
            var console = new Console(agent);
            console.Run();
        }
    }
}

using Regulus.Utility.WindowConsoleAppliction;

namespace Regulus.Samples.Chat1.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new Application(args[0] , new System.Net.IPEndPoint(System.Net.IPAddress.Parse(args[1]) , int.Parse(args[2])  ));
            app.Run();
        }
    }
}

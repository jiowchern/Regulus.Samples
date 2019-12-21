using System;
using Regulus.Utility.WindowConsoleAppliction;

namespace Regulus.Samples.Chat1.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new Application(int.Parse(args[0]));
            
            app.Run();
            
        }
    }
}


using Stride.Engine;

namespace Regulus.Samples.Chat1.Stride
{
    class Regulus_Samples_Chat1_StrideApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}

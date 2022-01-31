using Regulus.Samples.Chat1.Common;

namespace Regulus.Samples.Chat1
{
    public interface IHistroyable
    {
        Message[] Query();
    }
}
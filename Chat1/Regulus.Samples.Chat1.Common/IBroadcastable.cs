namespace Regulus.Samples.Chat1.Common
{
    public interface IBroadcastable
    {
        event System.Action<string, string> MessageEvent;
    }
}

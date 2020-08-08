namespace Regulus.Samples.Chat1.Common
{
    public interface IPlayer 
    {        
        event System.Action<string, string> PublicMessageEvent;
        event System.Action<string, string> PrivateMessageEvent;

        Regulus.Remote.INotifier<IChatter> Chatters { get; }
        void Send(string message);
        void Quit();
    }
    
}

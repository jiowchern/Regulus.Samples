namespace Regulus.Samples.Chat1.Common
{
    public interface IPlayer 
    {        
        event System.Action<Message> PublicMessageEvent;
        event System.Action<Message> PrivateMessageEvent;

        

        Regulus.Remote.Notifier<IChatter> Chatters { get; }
        void Send(string message);
        void Quit();
    }
    
}

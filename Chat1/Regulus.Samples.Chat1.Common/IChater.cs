namespace Regulus.Samples.Chat1.Common
{
    public interface IChatter
    {
        Regulus.Remote.Property<string> Name { get; }
        void Whisper(string message);
    }
    
}

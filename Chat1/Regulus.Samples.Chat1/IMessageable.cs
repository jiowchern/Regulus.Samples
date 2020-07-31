namespace Regulus.Samples.Chat1
{
    interface IMessageable
    {
        string Name { get; }

        void PublicReceive(string name, string message);
        void PrivateReceive(string name, string message);
    }
}
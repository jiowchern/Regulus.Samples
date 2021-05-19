using Regulus.Remote;
using Regulus.Samples.Chat1.Common;

namespace Regulus.Samples.Chat1
{
    internal class WhispeableChatter : IChatter
    {
        private readonly Chatter _Sender;
        private Chatter _Receiver;

        public WhispeableChatter(Chatter sender,Chatter chatter)
        {
            this._Sender = sender;
            this._Receiver = chatter;
        }

        Property<string> IChatter.Name => new Property<string>(_Receiver.Messager.Name);

        void IChatter.Whisper(string message)
        {            
            _Receiver.Messager.PrivateReceive(new Common.Message() { Name = _Sender.Messager.Name  , Context = message });
        }
    }
}
using Regulus.Remote;

namespace Regulus.Samples.Chat1.Common
{
    public static partial class ProtocolCreator
    {
        public static IProtocol Create()
        {
            Regulus.Remote.IProtocol p = null;
            _Create(ref p);
            return p;
        }

        [Regulus.Remote.Protocol.Creator]
        static partial void _Create(ref IProtocol p);
        
    }

}

namespace Regulus.Samples.Helloworld.Common
{
    public static partial class ProtocolCreator
    {
        public static Regulus.Remote.IProtocol Create()
        {
            Regulus.Remote.IProtocol p = null;
            _Create(ref p);
            return p;
        }

        [Regulus.Remote.Protocol.Creator]
        static partial void _Create(ref Regulus.Remote.IProtocol p);
    }
}

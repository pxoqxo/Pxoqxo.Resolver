namespace Pxoqxo.Resolver
{
    public sealed class ResolverStatusEventArgs : EventArgs
    {
        public Pack Pack { get; }
        public ResolverMessage Message { get; }
        public ResolverMessageType Type { get; }

        public ResolverStatusEventArgs(Pack pack, ResolverMessage message, ResolverMessageType type)
        {
            Pack = pack;
            Message = message;
            Type = type;
        }
    }
}

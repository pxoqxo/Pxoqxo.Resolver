namespace Pxoqxo.Resolver
{
    public sealed class ResolvingEventArgs : EventArgs
    {
        public bool Cancel { set; get; } = false;
    }
}

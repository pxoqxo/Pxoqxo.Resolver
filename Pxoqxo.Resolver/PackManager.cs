namespace Pxoqxo.Resolver
{
    public abstract class PackManager
    {
        public abstract string Name { get; }
        public abstract string BaseUrl { get; }
        public abstract string BasePath { get; }

        public abstract string GetPackUrl(Pack pack);
        public abstract string GetPackPath(Pack pack);
    }
}

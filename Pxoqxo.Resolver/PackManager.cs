namespace Pxoqxo.Resolver
{
    public sealed class PackManager
    {
        public string Name { get; }
        public string BaseUrl { get; }
        public string BasePath { get; }

        public PackManager(string name, string baseUrl, string basePath)
        {
            Name = name;
            BaseUrl = baseUrl;
            BasePath = basePath;
        }
    }
}

namespace Pxoqxo.Resolver
{
    public sealed class OxPackManager : PackManager
    {
        public override string Name { get; }
        public override string BaseUrl { get; }
        public override string BasePath { get; }

        public OxPackManager()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var folderPath = Environment.GetFolderPath(folder);
            var oxPath = Path.Combine(folderPath, "0xPack");

            Name = "0xPack";
            BaseUrl = "https://github.com/0xPack/";
            BasePath = oxPath;
        }

        public override string GetPackUrl(Pack pack)
        {
            Uri baseUrl = new Uri(BaseUrl);
            string extension = Path.GetExtension(pack.File);

            return new Uri(baseUrl, $"{pack.Entity}/releases/download/{pack.Name}/{pack.Version}-{pack.Rid}{extension}").ToString();
        }
        public override string GetPackPath(Pack pack)
        {
            return Path.Combine(BasePath, pack.Entity, pack.Name, pack.Version, pack.Rid, pack.File);
        }
    }
}

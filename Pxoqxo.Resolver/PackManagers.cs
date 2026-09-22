namespace Pxoqxo.Resolver
{
    public static class PackManagers
    {
        public static IEnumerable<PackManager> GetAll()
        {
            return new List<PackManager>()
            {
                new PackManager("0xPack", "https://0xpack.github.io/", Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "0xPack"
                ))
            };
        }
    }
}

namespace Pxoqxo.Resolver
{
    public sealed class PackManagers
    {
        private readonly Dictionary<string, PackManager> managers;
        private readonly HashSet<string> names;
        private readonly HashSet<string> baseUrls;
        private readonly HashSet<string> basePaths;

        public PackManagers()
        {
            managers = new Dictionary<string, PackManager>();
            names = new HashSet<string>();
            baseUrls = new HashSet<string>();
            basePaths = new HashSet<string>();
        }
        public void Add(PackManager item)
        {
            ThrowIfNotUnique(item);

            managers.Add(item.Name, item);
            names.Add(item.Name);
            baseUrls.Add(item.BaseUrl);
            basePaths.Add(item.BasePath);
        }
        public bool Remove(PackManager item)
        {
            if (!managers.Remove(item.Name))
            {
                return false;
            }

            names.Remove(item.Name);
            baseUrls.Remove(item.BaseUrl);
            basePaths.Remove(item.BasePath);
            return true;
        }
        public void Clear()
        {
            managers.Clear();
            names.Clear();
            baseUrls.Clear();
            basePaths.Clear();
        }
        public PackManager? Get(string name)
        {
            if (managers.TryGetValue(name, out PackManager? item))
            {
                return item;
            }

            return null;
        }

        public static PackManagers GetDefault()
        {
            PackManagers manager = new PackManagers();

            string localFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            manager.Add(new PackManager("0xPack", "https://0xPack.github.io/", Path.Combine(localFolder, "0xPack")));

            return manager;
        }

        private void ThrowIfNotUnique(PackManager item)
        {
            if (names.Contains(item.Name))
            {
                throw new InvalidOperationException($"A PackManager with the name '{item.Name}' already exists.");
            }

            if (baseUrls.Contains(item.BaseUrl))
            {
                throw new InvalidOperationException($"A PackManager with the base URL '{item.BaseUrl}' already exists.");
            }

            if (basePaths.Contains(item.BasePath))
            {
                throw new InvalidOperationException($"A PackManager with the base path '{item.BasePath}' already exists.");
            }
        }
    }
}

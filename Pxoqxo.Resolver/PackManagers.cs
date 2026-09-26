namespace Pxoqxo.Resolver
{
    public sealed class PackManagers
    {
        private bool isLocked = false;

        private readonly Dictionary<string, PackManager> managers;
        private readonly HashSet<string> names;
        private readonly HashSet<string> baseUrls;
        private readonly HashSet<string> basePaths;

        public bool IsLocked => isLocked;

        public PackManagers()
        {
            managers = new Dictionary<string, PackManager>();
            names = new HashSet<string>();
            baseUrls = new HashSet<string>();
            basePaths = new HashSet<string>();
        }
        public void Add(PackManager item)
        {
            ThrowIfLocked();
            ThrowIfInvalid(item);
            ThrowIfNotUnique(item);

            managers.Add(item.Name, item);
            names.Add(item.Name);
            baseUrls.Add(item.BaseUrl);
            basePaths.Add(item.BasePath);
        }
        public bool Remove(PackManager item)
        {
            ThrowIfLocked();

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
            ThrowIfLocked();

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
        public ICollection<PackManager> GetAll()
        {
            return managers.Values;
        }
        public void Lock()
        {
            isLocked = true;
        }

        internal static PackManagers GetDefault()
        {
            PackManagers manager = new PackManagers();
            manager.Add(new OxPackManager());
            manager.Lock();
            return manager;
        }

        private void ThrowIfLocked()
        {
            if (isLocked)
            {
                throw new InvalidOperationException("PackManagers object is locked.");
            }
        }
        private void ThrowIfInvalid(PackManager item)
        {
            if (string.IsNullOrEmpty(item.Name) || string.IsNullOrWhiteSpace(item.Name))
            {
                throw new ArgumentException("Item name is required.", nameof(item.Name));
            }
            if (string.IsNullOrEmpty(item.BaseUrl) || string.IsNullOrWhiteSpace(item.BaseUrl))
            {
                throw new ArgumentException("Item base URL is required.", nameof(item.BaseUrl));
            }
            if (string.IsNullOrEmpty(item.BasePath) || string.IsNullOrWhiteSpace(item.BasePath))
            {
                throw new ArgumentException("Item base path is required.", nameof(item.BasePath));
            }
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

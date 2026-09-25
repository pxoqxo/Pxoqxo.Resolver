using System.Runtime.Loader;

namespace Pxoqxo.Resolver
{
    public sealed class PackResolver
    {
        public Packs Packs { get; }
        public PackManagers Managers { get; }
        public AssemblyLoadContext Context { get; }

        public PackResolver(Packs packs) : this(packs, PackManagers.GetDefault(), AssemblyLoadContext.Default) { }
        public PackResolver(Packs packs, PackManagers managers) : this(packs, managers, AssemblyLoadContext.Default) { }
        public PackResolver(Packs packs, AssemblyLoadContext context) : this(packs, PackManagers.GetDefault(), context) { }
        public PackResolver(Packs packs, PackManagers managers, AssemblyLoadContext context)
        {
            if (!packs.IsLocked)
            {
                throw new InvalidOperationException("Cannot initialize PackResolver because the Packs object is not locked.");
            }
            if (!managers.IsLocked)
            {
                throw new InvalidOperationException("Cannot initialize PackResolver because the PackManagers object is not locked.");
            }

            Packs = packs;
            Managers = managers;
            Context = context;
        }
        public void Resolve()
        {
            ICollection<Pack> packs = Packs.GetAll();
            foreach (Pack pack in packs)
            {
                Resolve(pack);
            }
        }

        private void Resolve(Pack pack)
        {
            string managerName = pack.Manager;
            PackManager? manager = Managers.Get(managerName);
            if (manager == null)
            {
                throw new KeyNotFoundException($"No PackageManager found having name '{managerName}'.");
            }

            string url = GetPackUrl(pack, manager);
            string path = GetPackPath(pack, manager);
        }
        private string GetPackUrl(Pack pack, PackManager manager)
        {
            Uri baseUrl = new Uri(manager.BaseUrl);
            return new Uri(baseUrl,
                pack.Entity + "/" +
                pack.Name + "/" +
                pack.Version + "/" +
                pack.Rid + "/" +
                pack.Path + "/" +
                pack.File).ToString();
        }
        private string GetPackPath(Pack pack, PackManager manager)
        {
            string basePath = manager.BasePath;
            return Path.Combine(basePath,
                pack.Entity,
                pack.Name,
                pack.Version,
                pack.Rid,
                pack.Path,
                pack.File);
        }
    }
}

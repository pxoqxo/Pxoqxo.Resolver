using System.Collections.Immutable;

namespace Pxoqxo.Resolver
{
    public sealed class PackResolverSettings
    {
        public bool AutoResolve { get; }
        public IImmutableList<PackManager> Managers { get; }

        public PackResolverSettings() : this(true, PackManagers.GetAll()) { }
        public PackResolverSettings(bool autoResolve) : this(autoResolve, PackManagers.GetAll()) { }
        public PackResolverSettings(IEnumerable<PackManager> managers) : this(true, managers) { }
        public PackResolverSettings(bool autoResolve, IEnumerable<PackManager> managers)
        {
            AutoResolve = autoResolve;
            Managers = managers.ToImmutableList();
        }
    }
}

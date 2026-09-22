namespace Pxoqxo.Resolver
{
    public sealed class PackResolverSettings
    {
        public bool AutoResolve { get; }
        public PackManagers Managers { get; }

        public PackResolverSettings() : this(true, PackManagers.GetDefault()) { }
        public PackResolverSettings(bool autoResolve) : this(autoResolve, PackManagers.GetDefault()) { }
        public PackResolverSettings(PackManagers managers) : this(true, managers) { }
        public PackResolverSettings(bool autoResolve, PackManagers managers)
        {
            AutoResolve = autoResolve;
            Managers = managers;
        }
    }
}

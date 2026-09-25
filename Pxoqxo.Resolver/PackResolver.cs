using System.Reflection;
using System.Runtime.Loader;

namespace Pxoqxo.Resolver
{
    public sealed class PackResolver : IDisposable
    {
        public AssemblyLoadContext Context { get; }
        public PackResolverSettings Settings { get; }
        public Packs Packs { get; }

        public PackResolver(Packs packs) : this(AssemblyLoadContext.Default, new PackResolverSettings(), packs) { }
        public PackResolver(AssemblyLoadContext context, Packs packs) : this(context, new PackResolverSettings(), packs) { }
        public PackResolver(PackResolverSettings settings, Packs packs) : this(AssemblyLoadContext.Default, settings, packs) { }
        public PackResolver(AssemblyLoadContext context, PackResolverSettings settings, Packs packs)
        {
            if (!settings.Managers.IsLocked)
            {
                throw new InvalidOperationException("Cannot initialize PackResolver because the PackManagers object is not locked.");
            }
            if (!packs.IsLocked)
            {
                throw new InvalidOperationException("Cannot initialize PackResolver because the Packs object is not locked.");
            }

            Context = context;
            Settings = settings;
            Packs = packs;

            if (!Settings.AutoResolve)
            {
                return;
            }

            Context.Resolving += Context_Resolving;
            Context.ResolvingUnmanagedDll += Context_ResolvingUnmanagedDll;
        }
        public void Dispose()
        {
            if (!Settings.AutoResolve)
            {
                return;
            }

            Context.Resolving -= Context_Resolving;
            Context.ResolvingUnmanagedDll -= Context_ResolvingUnmanagedDll;
        }

        public void Resolve()
        {

        }

        private Assembly? Context_Resolving(AssemblyLoadContext arg1, AssemblyName arg2)
        {
            return null;
        }
        private nint Context_ResolvingUnmanagedDll(Assembly arg1, string arg2)
        {
            return nint.Zero;
        }
    }
}

using System.Reflection;
using System.Runtime.Loader;

namespace Pxoqxo.Resolver
{
    public sealed class PackResolver : IDisposable
    {
        public AssemblyLoadContext Context { get; }
        public PackResolverSettings Settings { get; }

        public PackResolver() : this(AssemblyLoadContext.Default, new PackResolverSettings()) { }
        public PackResolver(AssemblyLoadContext context) : this(context, new PackResolverSettings()) { }
        public PackResolver(PackResolverSettings settings) : this(AssemblyLoadContext.Default, settings) { }
        public PackResolver(AssemblyLoadContext context, PackResolverSettings settings)
        {
            Context = context;
            Settings = settings;

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

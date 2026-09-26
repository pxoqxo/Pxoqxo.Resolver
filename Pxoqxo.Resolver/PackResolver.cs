using Pxoqxo.Ext.Core;
using System.Runtime.InteropServices;
using System.Runtime.Loader;

namespace Pxoqxo.Resolver
{
    public sealed class PackResolver : IDisposable
    {
        private readonly HttpClient client = new HttpClient();

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

            client = new HttpClient();

            Packs = packs;
            Managers = managers;
            Context = context;
        }
        public void Dispose()
        {
            client.Dispose();
        }
        public async Task Resolve(bool force = false)
        {
            ResolvingEventArgs args = new ResolvingEventArgs();
            OnResolving(args);

            if (args.Cancel)
            {
                return;
            }

            ICollection<Pack> packs = Packs.GetAll();
            foreach (Pack pack in packs)
            {
                await Resolve(pack, force);
            }

            OnResolved(new ResolvedEventArgs());
        }

        public event EventHandler<ResolvingEventArgs>? Resolving;
        public event EventHandler<ResolverStatusEventArgs>? ResolverStatus;
        public event EventHandler<ResolvedEventArgs>? Resolved;

        private void OnResolving(ResolvingEventArgs args)
        {
            Resolving?.Invoke(this, args);
        }
        private void OnResolverStatus(ResolverStatusEventArgs args)
        {
            ResolverStatus?.Invoke(this, args);
        }
        private void OnResolved(ResolvedEventArgs args)
        {
            Resolved?.Invoke(this, args);
        }

        private async Task Resolve(Pack pack, bool force)
        {
            OnResolverStatus(new ResolverStatusEventArgs(pack, ResolverMessage.Started, ResolverMessageType.Normal));

            string managerName = pack.Manager;
            PackManager? manager = Managers.Get(managerName);
            if (manager == null)
            {
                OnResolverStatus(new ResolverStatusEventArgs(pack, ResolverMessage.Failed, ResolverMessageType.Error));
                return;
            }

            string url = manager.GetPackUrl(pack);
            string path = manager.GetPackPath(pack);

            if (!await DownloadIfNotExists(url, path, force))
            {
                OnResolverStatus(new ResolverStatusEventArgs(pack, ResolverMessage.Failed, ResolverMessageType.Error));
                return;
            }

            if (!LoadPack(pack, path))
            {
                OnResolverStatus(new ResolverStatusEventArgs(pack, ResolverMessage.Failed, ResolverMessageType.Error));
                return;
            }

            OnResolverStatus(new ResolverStatusEventArgs(pack, ResolverMessage.Finished, ResolverMessageType.Success));
        }
        private async Task<bool> DownloadIfNotExists(string url, string path, bool force)
        {
            if (!force && File.Exists(path))
            {
                return true;
            }

            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            return false;
                        }

                        string? directory = Path.GetDirectoryName(path);
                        if (!directory.IsNullOrEmpty() && !Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }

                        using (var remoteStream = await response.Content.ReadAsStreamAsync())
                        {
                            using (var localStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 1048576, true))
                            {
                                await remoteStream.CopyToAsync(localStream);
                                return true;
                            }
                        }
                    }
                }
            }
            catch
            {
                DeleteDownloaded(path);
                return false;
            }
        }
        private void DeleteDownloaded(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }

            try
            {
                File.Delete(path);
            }
            catch
            {
                // Empty
            }
        }
        private bool LoadPack(Pack pack, string path)
        {
            try
            {
                if (pack.Type == PackType.ManagedLibrary)
                {
                    Context.LoadFromAssemblyPath(path);
                }
                else if (pack.Type == PackType.UnmanagedLibrary)
                {
                    NativeLibrary.Load(path);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

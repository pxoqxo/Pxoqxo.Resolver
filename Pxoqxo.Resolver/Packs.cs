using System.Collections.ObjectModel;

namespace Pxoqxo.Resolver
{
    public sealed class Packs
    {
        private bool isLocked = false;

        private readonly List<Pack> packs;

        public bool IsLocked => isLocked;

        public Packs()
        {
            packs = new List<Pack>();
        }
        public void Add(Pack pack)
        {
            ThrowIfLocked();
            ThrowIfInvalid(pack);
            packs.Add(pack);
        }
        public bool Remove(Pack pack)
        {
            ThrowIfLocked();
            return packs.Remove(pack);
        }
        public void Clear()
        {
            ThrowIfLocked();
            packs.Clear();
        }
        public Pack Get(int index)
        {
            return packs[index];
        }
        public ICollection<Pack> GetAll()
        {
            return new ReadOnlyCollection<Pack>(packs);
        }
        public void Lock()
        {
            isLocked = true;
        }

        private void ThrowIfLocked()
        {
            if (isLocked)
            {
                throw new InvalidOperationException("Packs object is locked.");
            }
        }
        private void ThrowIfInvalid(Pack pack)
        {
            if (string.IsNullOrEmpty(pack.Manager) || string.IsNullOrWhiteSpace(pack.Manager))
            {
                throw new ArgumentException("Pack manager is required.", nameof(pack.Manager));
            }
            if (string.IsNullOrEmpty(pack.Entity) || string.IsNullOrWhiteSpace(pack.Entity))
            {
                throw new ArgumentException("Pack entity is required.", nameof(pack.Entity));
            }
            if (string.IsNullOrEmpty(pack.Name) || string.IsNullOrWhiteSpace(pack.Name))
            {
                throw new ArgumentException("Pack name is required.", nameof(pack.Name));
            }
            if (string.IsNullOrEmpty(pack.Version) || string.IsNullOrWhiteSpace(pack.Version))
            {
                throw new ArgumentException("Pack version is required.", nameof(pack.Version));
            }
            if (string.IsNullOrEmpty(pack.Rid) || string.IsNullOrWhiteSpace(pack.Rid))
            {
                throw new ArgumentException("Pack RID is required.", nameof(pack.Rid));
            }
            if (string.IsNullOrEmpty(pack.File) || string.IsNullOrWhiteSpace(pack.File))
            {
                throw new ArgumentException("Pack file is required.", nameof(pack.File));
            }
            if (!Enum.IsDefined(pack.Type))
            {
                throw new ArgumentOutOfRangeException(nameof(pack.Type), pack.Type, "Pack type is invalid.");
            }
            if (pack.Type == PackType.Unknown)
            {
                throw new ArgumentException("Pack type cannot be Unknown.", nameof(pack.Type));
            }
        }
    }
}

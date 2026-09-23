using System.Collections.ObjectModel;

namespace Pxoqxo.Resolver
{
    public sealed class Packs
    {
        private bool isLocked = false;

        private readonly List<Pack> packs;

        public Packs()
        {
            packs = new List<Pack>();
        }
        public void Add(Pack pack)
        {
            ThrowIfLocked();
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
    }
}

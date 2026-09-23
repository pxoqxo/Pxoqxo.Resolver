namespace Pxoqxo.Resolver
{
    public sealed class PackBuilder
    {
        private readonly Pack pack;

        private PackBuilder(Pack pack)
        {
            this.pack = pack;
        }
        public static PackBuilder Create()
        {
            Pack pack = new Pack();
            return new PackBuilder(pack);
        }
        public Pack Build()
        {
            return pack;
        }

        public PackBuilder Manager(string value)
        {
            Pack clone = pack.Clone();
            clone.Manager = value;

            return new PackBuilder(clone);
        }
        public PackBuilder Entity(string value)
        {
            Pack clone = pack.Clone();
            clone.Entity = value;

            return new PackBuilder(clone);
        }
        public PackBuilder Name(string value)
        {
            Pack clone = pack.Clone();
            clone.Name = value;

            return new PackBuilder(clone);
        }
        public PackBuilder Version(string value)
        {
            Pack clone = pack.Clone();
            clone.Version = value;

            return new PackBuilder(clone);
        }
        public PackBuilder Rid(string value)
        {
            Pack clone = pack.Clone();
            clone.Rid = value;

            return new PackBuilder(clone);
        }
        public PackBuilder Path(string value)
        {
            Pack clone = pack.Clone();
            clone.Path = value;

            return new PackBuilder(clone);
        }
        public PackBuilder File(string value)
        {
            Pack clone = pack.Clone();
            clone.File = value;

            return new PackBuilder(clone);
        }
        public PackBuilder Type(PackType value)
        {
            Pack clone = pack.Clone();
            clone.Type = value;

            return new PackBuilder(clone);
        }
    }
}

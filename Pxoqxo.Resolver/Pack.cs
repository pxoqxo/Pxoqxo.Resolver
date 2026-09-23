namespace Pxoqxo.Resolver
{
    public sealed class Pack
    {
        public string Manager { internal set; get; } = string.Empty;
        public string Entity { internal set; get; } = string.Empty;
        public string Name { internal set; get; } = string.Empty;
        public string Version { internal set; get; } = string.Empty;
        public string Rid { internal set; get; } = string.Empty;
        public string Path { internal set; get; } = string.Empty;
        public string File { internal set; get; } = string.Empty;
        public PackType Type { internal set; get; } = PackType.Unknown;

        internal Pack() { }
        internal Pack Clone()
        {
            return new Pack()
            {
                Manager = Manager,
                Entity = Entity,
                Name = Name,
                Version = Version,
                Rid = Rid,
                Path = Path,
                File = File,
                Type = Type
            };
        }
    }
}

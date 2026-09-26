using Pxoqxo.Resolver;

Packs packs = new Packs();
PackBuilder mainBuilder = PackBuilder.Create().
    Manager("0xPack").
    Entity("Pxoqxo").
    Version("em1.0").
    Rid("portable").
    Type(PackType.ManagedLibrary);

packs.Add(mainBuilder.Name("Pxoqxo.Quick").File("Pxoqxo.Quick.dll").Build());
packs.Lock();

PackResolver resolver = new PackResolver(packs);
resolver.Resolving += (sender, e) =>
{
    Console.WriteLine("Resolving");

    // Stop Resolving.
    e.Cancel = true;

    // Continue Resolving (default).
    e.Cancel = false;
};
resolver.ResolverStatus += (sender, e) =>
{
    if (e.Type == ResolverMessageType.Normal)
    {
        Console.ForegroundColor = ConsoleColor.White;
    }
    else if (e.Type == ResolverMessageType.Warning)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
    }
    else if (e.Type == ResolverMessageType.Error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }
    else if (e.Type == ResolverMessageType.Success)
    {
        Console.ForegroundColor = ConsoleColor.Green;
    }

    Console.WriteLine($"Pack: {e.Pack.Name}, Message: {e.Message}, MessageType: {e.Type}");
    Console.ResetColor();
};
resolver.Resolved += (sender, e) =>
{
    Console.WriteLine("Resolved");
};
resolver.Resolve().Wait();

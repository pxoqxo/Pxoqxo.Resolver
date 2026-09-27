using Pxoqxo.Ext.Core;
using Pxoqxo.Quick;
using Pxoqxo.Resolver;
using Pxoqxo.UnitTest;
using System.Runtime.InteropServices;

Packs packs = new Packs();
PackBuilder mainBuilder = PackBuilder.Create()
    .Manager("0xPack")
    .Entity("Pxoqxo")
    .Version("em1.0")
    .Rid("portable")
    .Type(PackType.ManagedLibrary);

packs.Add(mainBuilder.Name("Pxoqxo.Ext").File("Pxoqxo.Ext.Core.dll").Build());
packs.Add(mainBuilder.Name("Pxoqxo.Quick").File("Pxoqxo.Quick.dll").Build());
packs.Add(mainBuilder.Name("Pxoqxo.UnitTest").File("Pxoqxo.UnitTest.dll").Build());
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
await resolver.Resolve();

OtherBlock();// <- You can call your application logic here. Do not use direct logic.

Console.WriteLine("Press any key to exit...");
Console.ReadKey();

void OtherBlock()
{
    Test.Run(() =>
    {
        // DISCLAIMER:
        // Compile-time: Ensure your assembly references point strictly to the 
        // HintPath locations defined in Pxoqxo.Resolver.Tests.csproj.
        // 
        // Runtime: If any dependencies are missing from the configuration above, 
        // the Resolver will automatically try to resolve them at application startup.

        var jsonObj = new { Name = "pxoqxo", ObjectType = "Anon" };
        string? json = QuickJson.ToJson(jsonObj);
        if (json.IsNullOrEmptyOrWhiteSpace())
        {
            return false;
        }

        Console.WriteLine("JSON: " + json);
        return true;
    });
    Test.Run(() =>
    {
        WindowsManagedCode managedCode = new WindowsManagedCode();
        managedCode.MoveMouseLeftToRight();
        return true;
    });
}
public sealed class WindowsManagedCode
{
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);

    public void MoveMouseLeftToRight()
    {
        for (int x = 0; x <= 2000; x += 10)
        {
            SetCursorPos(x, 500);
            Thread.Sleep(10);
        }
    }
}

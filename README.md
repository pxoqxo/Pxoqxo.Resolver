# 📦 Pxoqxo.Resolver
**Pxoqxo.Resolver** is a self-hosted package management system written in C# 💖 .NET. It manages both managed and unmanaged libraries. Define dependencies at application startup, and it handles the rest.

## ⚡ How It Works
- First checks the local path for available packages.
- If not found locally, downloads to the user's local package directory.

## 💡 Benefits
- Smaller distributed binaries when multiple applications share the same dependency.
- Saves space on end-users' computers through deduplication.

## 📃 Key Features
- Fully **FOSS (Free and Open Source Software)**.
- Cross-platform package management for .NET projects.
- Links all dependencies at startup time.
- Event-driven architecture.
- Supports multiple package managers and custom AssemblyLoadContext.
- **Force flag option:** Redownloads corrupted packages instead of checking local availability.

## 📙 Usage Example

```csharp
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
```

### 🔎 Important Notes
- **Use at application startup:** Implement this code strictly during the application startup phase.
- **Separate application logic:** Never include actual application logic within the startup block. Create a separate function and call it after invoking `PackResolver.Resolve()`.
- **Build-time dependency requirement:** Ensure that all required dependencies exist in the exact same folder where the application output is generated during compilation.
- **Explicit runtime registration:** The runtime environment will attempt to resolve dependencies automatically, but you must explicitly add all packs to the resolver. It will not automatically resolve transitive dependencies (a dependency's own dependencies).

## 🏭 Use dependencies from a local centralized location during development
You can add direct dependencies from a local path where all packages are stored centrally.

```csharp
<LocalAppData>$([System.Environment]::GetFolderPath(SpecialFolder.LocalApplicationData))</LocalAppData>
```

Instead of...

```csharp
<Reference Include="Pxoqxo.Ext.Core">
    <HintPath>..\..\publish\Pxoqxo.Ext\em1.0\portable\Pxoqxo.Ext.Core.dll</HintPath>
</Reference>
```

Use...

```csharp
<Reference Include="Pxoqxo.Ext.Core">
    <HintPath>$(LocalAppData)\0xPack\pxoqxo\Pxoqxo.Ext\em1.0\portable\Pxoqxo.Ext.Core.dll</HintPath>
</Reference>
```

## 🛠️ Prerequisites
- **Pxoqxo.Resolver (em1.0)**
  - .NET 8 SDK (or newer)
- **Pxoqxo.Resolver.Tests (em1.0)**
  - .NET 8 SDK (or newer)
  - Pxoqxo.Resolver (em1.0)
  - Pxoqxo.Quick (em1.0)
  - Pxoqxo.UnitTest (em1.0)
  - Pxoqxo.Ext (em1.0)
    - Pxoqxo.Ext.Core

## 🤝 Contributing
We love **FOSS** contributions! At this time, contributions are accepted only through email. If you'd like to report a bug, suggest a feature, or contribute code, please contact me via pxoqxo@atomicmail.io.

## 📜 License
This project is licensed under the MIT License. See the LICENSE file for the full license text.

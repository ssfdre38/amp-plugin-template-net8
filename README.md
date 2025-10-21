# AMP Plugin Template for .NET 8

This is an updated plugin template for CubeCoders AMP 2.5+ which uses .NET 8 instead of .NET Framework 4.8.

## Prerequisites

- Visual Studio 2022 (17.8 or later)
- .NET 8 SDK
- AMP 2.5 or later installed

## Getting Started

### 1. Create New Plugin

1. Clone or download this template
2. Open the `.csproj` file in Visual Studio
3. Replace all `$safeprojectname$` placeholders with your plugin name
4. Replace `$author$`, `$company$`, etc. with your information
5. Generate a new GUID for `PluginID` in `PluginMain.cs`

### 2. Configure AMP Reference

The template references `ModuleShared.dll` from AMP. You need to set the path:

**Option A: Environment Variable (Recommended)**
```cmd
setx AMPINSTALLDIR "C:\Path\To\Your\AMP\Installation"
```

**Option B: Direct Path**
Edit the `.csproj` file and replace:
```xml
<HintPath>$(AMPINSTALLDIR)\ModuleShared.dll</HintPath>
```
with:
```xml
<HintPath>C:\Path\To\Your\AMP\Installation\ModuleShared.dll</HintPath>
```

### 3. Build the Plugin

```bash
dotnet build -c Release
```

The output will be in `bin/Release/net8.0/`

### 4. Deploy to AMP

Copy the following files to your AMP plugins directory:
- `YourPlugin.dll`
- `WebRoot/` folder (if you have web interface)

**Windows:** `%ProgramData%\CubeCoders\AMP\instances\[InstanceName]\Plugins\YourPlugin\`
**Linux:** `/home/amp/.ampdata/instances/[InstanceName]/Plugins/YourPlugin/`

### 5. Restart AMP

Restart your AMP instance to load the new plugin:
```bash
ampinstmgr restart [InstanceName]
```

## Project Structure

```
YourPlugin/
├── PluginMain.cs           # Main plugin class (inherits from AMPPlugin)
├── YourPlugin.csproj       # .NET 8 project file
├── WebRoot/                # Web interface files (optional)
│   ├── Plugin.html         # Main HTML interface
│   ├── CSS/
│   │   └── Plugin.css      # Stylesheet (dark theme)
│   └── Scripts/
│       └── Plugin.js       # Frontend JavaScript
└── README.md              # This file
```

## Plugin Components

### PluginMain.cs

Main plugin class that AMP loads. Key members:

- **Metadata Properties:**
  - `DisplayName` - Name shown in AMP UI
  - `Author` - Your name
  - `Description` - Plugin description
  - `PluginID` - Unique GUID
  - `PluginVersion` - Version number
  - `HasWebRoot` - Set to `true` if you have web interface
  - `WebRootPath` - Path to WebRoot folder

- **Lifecycle Methods:**
  - `Init()` - Called when plugin loads
  - `Shutdown()` - Called when plugin unloads

- **Web API Methods:**
  - Decorated with `[WebMethod]` attribute
  - Accessible at `/API/YourPlugin/MethodName`
  - Return `ActionResult` objects

### Web Interface

- **HTML** - Located in `WebRoot/Plugin.html`
- **CSS** - Dark theme matching AMP's style
- **JavaScript** - Helper functions for API calls

## Example: Creating a Web API Method

```csharp
[WebMethod("Gets server status", "getStatus")]
public ActionResult GetStatus()
{
    try
    {
        return new ActionResult
        {
            Status = ActionStatus.OK,
            Result = new
            {
                success = true,
                serverName = "MyServer",
                uptime = TimeSpan.FromHours(24)
            }
        };
    }
    catch (Exception ex)
    {
        return new ActionResult
        {
            Status = ActionStatus.Failure,
            Reason = ex.Message
        };
    }
}
```

Call from JavaScript:
```javascript
const result = await callAPI('GetStatus');
console.log(result);
```

## Configuration Menu Integration

Your plugin can appear in AMP's Configuration menu by registering it in JavaScript:

```javascript
if (window.AMP && window.AMP.registerPlugin) {
    window.AMP.registerPlugin({
        name: 'YourPlugin',
        displayName: 'Your Plugin',
        category: 'Configuration',
        icon: 'fa-plug',
        route: '/Plugin.html',
        permission: 'Core.WebPanel.ViewPlugins'
    });
}
```

## Best Practices

1. **Error Handling** - Always wrap API methods in try-catch
2. **Logging** - Use `Console.WriteLine()` for debugging
3. **Permissions** - Check user permissions before sensitive operations
4. **API Design** - Keep API methods simple and focused
5. **Documentation** - Comment your code and API methods
6. **Testing** - Test on both Windows and Linux if targeting both platforms

## Debugging

### Enable Debug Output
```csharp
public override void Init(IApplicationWrapper app, ProviderManifest manifest)
{
    base.Init(app, manifest);
    Console.WriteLine($"[YourPlugin] Debug: Initializing...");
}
```

### Check AMP Logs
**Windows:** `%ProgramData%\CubeCoders\AMP\instances\[InstanceName]\AMP_Logs\`
**Linux:** `/home/amp/.ampdata/instances/[InstanceName]/AMP_Logs/`

## Common Issues

### Plugin Not Loading
- Check that your DLL is in the correct plugins directory
- Verify the DLL is named exactly like the directory
- Check AMP logs for errors
- Ensure `PluginMain` class exists and inherits from `AMPPlugin`

### Web Interface Not Accessible
- Verify `HasWebRoot = true` in PluginMain.cs
- Check that WebRoot folder is deployed with DLL
- Ensure plugin is actually loaded (check logs)

### API Methods Not Working
- Verify `[WebMethod]` attribute is present
- Check method is `public`
- Ensure return type is `ActionResult`
- Test API endpoint: `http://youramp:port/API/YourPlugin/MethodName`

## Differences from .NET Framework 4.8 Template

### What's New
- ✅ Target Framework: `net8.0`
- ✅ Uses SDK-style project format
- ✅ Nullable reference types enabled
- ✅ Implicit usings enabled
- ✅ Modern C# language features available

### What's Changed
- Project file is now SDK-style (simpler)
- No `packages.config` (uses PackageReference)
- Different build output structure
- Modern async/await patterns recommended

### What's Removed
- No legacy .NET Framework dependencies
- No app.config (use appsettings.json if needed)
- No AssemblyInfo.cs (properties in .csproj)

## Support

- **AMP Documentation:** https://cubecoders.com/AMPdocs
- **AMP Support:** https://cubecoders.com/support
- **AMP Discord:** Join for community support
- **GitHub Issues:** https://github.com/CubeCoders/AMP/issues

## License

This template is provided for use with CubeCoders AMP. Check AMP's license terms for plugin development and distribution.

## Contributing

If you find issues or have improvements for this template, please submit them to the AMP GitHub repository.

---

**Template Version:** 1.0.0 (.NET 8)  
**Compatible with:** AMP 2.5+  
**Last Updated:** October 2025

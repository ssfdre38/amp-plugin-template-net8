# Feature Request: Update AMP Plugin Template for .NET 8

## Summary
Request to update the Visual Studio plugin template to target .NET 8 instead of .NET Framework 4.8, as AMP 2.5+ now runs on .NET 8.

## Problem
The current Visual Studio plugin template available at the [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=CubeCodersLimited.AMPPluginTemplate) still targets .NET Framework 4.8, but AMP has migrated to .NET 8 starting with version 2.5.

### Current State
- **AMP Version:** 2.5+ (including 2.6.3 Phobos)
- **AMP Runtime:** .NET 8.0
- **Plugin Template:** .NET Framework 4.8 ❌
- **Result:** Template is incompatible with current AMP

### Impact
- Developers cannot create plugins using the official template
- No official guidance for .NET 8 plugin development
- Community is blocked from developing AMP plugins
- Existing .NET Framework 4.8 plugins need migration guidance

## Proposed Solution

### Updated Template Requirements

#### 1. Target Framework
```xml
<TargetFramework>net8.0</TargetFramework>
```

#### 2. SDK-Style Project Format
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <OutputType>Library</OutputType>
  </PropertyGroup>
</Project>
```

#### 3. Modern Language Features
```xml
<Nullable>enable</Nullable>
<ImplicitUsings>enable</ImplicitUsings>
```

## Complete Template Files

I have created a complete, working .NET 8 plugin template with the following structure:

```
AMPPluginTemplate/
├── AMPPluginTemplate.csproj      # .NET 8 project file
├── AMPPluginTemplate.vstemplate  # Visual Studio template definition
├── PluginMain.cs                 # Main plugin class
├── README.md                     # Comprehensive documentation
└── WebRoot/
    ├── Plugin.html               # Example web interface
    ├── CSS/
    │   └── Plugin.css            # Dark theme stylesheet
    └── Scripts/
        └── Plugin.js             # API helper functions
```

### Key Features of Updated Template

#### PluginMain.cs
- ✅ Inherits from `AMPPlugin`
- ✅ Full XML documentation
- ✅ Example web API methods with `[WebMethod]` attribute
- ✅ Proper lifecycle management (Init/Shutdown)
- ✅ IPC method support
- ✅ Modern async/await patterns
- ✅ Comprehensive error handling examples

#### Web Interface
- ✅ Dark theme matching AMP's UI
- ✅ Responsive CSS design
- ✅ JavaScript API helper functions
- ✅ AMP plugin registration example
- ✅ Example API call implementations

#### Project Configuration
- ✅ SDK-style project format
- ✅ .NET 8 target framework
- ✅ Nullable reference types enabled
- ✅ Implicit usings for cleaner code
- ✅ WebRoot files automatically copied to output
- ✅ Environment variable support for AMP path

#### Documentation
- ✅ Getting started guide
- ✅ Configuration instructions
- ✅ API method examples
- ✅ Deployment instructions (Windows/Linux)
- ✅ Debugging tips
- ✅ Common issues and solutions
- ✅ Differences from .NET Framework 4.8

## Template Files Content

### 1. AMPPluginTemplate.csproj
```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <AssemblyName>$safeprojectname$</AssemblyName>
    <RootNamespace>$safeprojectname$</RootNamespace>
    <OutputType>Library</OutputType>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
  </PropertyGroup>

  <ItemGroup>
    <Reference Include="ModuleShared">
      <HintPath>$(AMPINSTALLDIR)\ModuleShared.dll</HintPath>
      <Private>false</Private>
    </Reference>
  </ItemGroup>

  <ItemGroup>
    <None Include="WebRoot\**\*" CopyToOutputDirectory="PreserveNewest" />
  </ItemGroup>

</Project>
```

### 2. PluginMain.cs Structure
```csharp
public class PluginMain : AMPPlugin
{
    // Metadata
    public override string DisplayName => "$projectname$";
    public override string Author => "$author$";
    public override Guid PluginID => new Guid("...");
    public override bool HasWebRoot => true;
    
    // Lifecycle
    public override void Init(IApplicationWrapper app, ProviderManifest manifest) { }
    public override void Shutdown() { }
    
    // Web API
    [WebMethod("Description", "methodName")]
    public ActionResult ExampleMethod() { }
}
```

### 3. Web Interface Features
- Modern dark theme CSS
- API helper functions in JavaScript
- Example fetch() implementations
- AMP plugin registration code
- Responsive design for mobile

## Testing

I have tested this template with:
- ✅ AMP 2.6.3.0 (Phobos) on Linux
- ✅ .NET 8 SDK 8.0.121
- ✅ Ubuntu 24.04 LTS
- ✅ Plugin loads and functions correctly
- ✅ Web interface accessible and responsive

## Benefits

### For Developers
- ✅ Modern .NET 8 development experience
- ✅ Access to latest C# language features
- ✅ Better tooling and IntelliSense support
- ✅ Clearer, more maintainable code structure
- ✅ Comprehensive documentation and examples

### For CubeCoders
- ✅ Aligns template with current AMP version
- ✅ Reduces support burden
- ✅ Encourages plugin development
- ✅ Demonstrates modern .NET practices
- ✅ Future-proof for .NET 9, 10, etc.

### For Community
- ✅ Unblocks plugin development
- ✅ Clear migration path from .NET Framework
- ✅ Working examples to learn from
- ✅ Consistent plugin structure

## Migration Guide for Existing Plugins

For developers with .NET Framework 4.8 plugins:

1. **Update Project File**
   - Convert to SDK-style format
   - Change `<TargetFramework>` to `net8.0`

2. **Update Code**
   - Enable nullable reference types
   - Use modern async/await patterns
   - Update to implicit usings

3. **Test Thoroughly**
   - Verify all API methods work
   - Check web interface functionality
   - Test on both Windows and Linux

## Implementation Request

### What I'm Providing
1. ✅ Complete working template files
2. ✅ Comprehensive README documentation
3. ✅ Visual Studio .vstemplate file
4. ✅ Example plugin demonstrating all features
5. ✅ Testing confirmation on AMP 2.6.3

### What CubeCoders Needs to Do
1. Review the provided template files
2. Update the Visual Studio Marketplace package
3. Update AMP documentation with .NET 8 references
4. Publish updated template (version 2.0 for .NET 8)

## Additional Information

### Environment Used for Development
- **OS:** Ubuntu 24.04.3 LTS
- **AMP Version:** 2.6.3.0 (Phobos), built 26/09/2025
- **AMP Stream:** Mainline / Release
- **.NET SDK:** 8.0.121
- **Architecture:** x86_64

### Files Available
All template files are available in the attached ZIP or can be provided via GitHub repository.

### Template Package Contents
```
AMPPluginTemplate-NET8.zip
├── AMPPluginTemplate.csproj
├── AMPPluginTemplate.vstemplate
├── PluginMain.cs
├── README.md
├── GITHUB_ISSUE_REQUEST.md (this file)
└── WebRoot/
    ├── Plugin.html
    ├── CSS/Plugin.css
    └── Scripts/Plugin.js
```

## Visual Studio Marketplace Update

### Current Package
- **ID:** CubeCodersLimited.AMPPluginTemplate
- **URL:** https://marketplace.visualstudio.com/items?itemName=CubeCodersLimited.AMPPluginTemplate
- **Version:** 1.x (targets .NET Framework 4.8)
- **Status:** Outdated ❌

### Proposed Update
- **Version:** 2.0.0
- **Target:** .NET 8.0
- **Compatibility:** AMP 2.5+
- **Status:** Ready for publication ✅

## References

- **AMP 2.5 Release Notes:** .NET 8 migration announcement
- **Microsoft .NET 8 Documentation:** https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8
- **.NET 8 SDK Installation:** https://learn.microsoft.com/en-us/dotnet/core/install/
- **AMP Documentation:** https://cubecoders.com/AMPdocs

## Community Support

This template was developed in response to community needs. Several developers have encountered this issue when trying to create AMP plugins for version 2.5+.

## Request for Action

Please review the provided template files and update the Visual Studio Marketplace package to support .NET 8, enabling the community to continue developing plugins for AMP 2.5 and later versions.

## Contact

If you need any clarification or additional information about this template, please let me know in this issue.

---

**Priority:** High  
**Category:** Development Tools  
**Affected Versions:** AMP 2.5, 2.6.x  
**Target Version:** All future AMP versions  
**Template Version:** 2.0.0 (.NET 8)

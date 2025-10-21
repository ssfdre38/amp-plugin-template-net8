# AMP Plugin Template - Installation Guide

## For Visual Studio Users

### Method 1: Install from VSIX (When Available)
1. Download `AMPPluginTemplate.vsix`
2. Double-click to install
3. Restart Visual Studio
4. Create new project → Search "AMP Plugin"

### Method 2: Manual Template Installation

#### Windows
1. Locate your Visual Studio templates folder:
   ```
   %USERPROFILE%\Documents\Visual Studio 2022\Templates\ProjectTemplates\
   ```

2. Create folder: `AMPPluginTemplate`

3. Copy all template files to this folder:
   - AMPPluginTemplate.csproj
   - AMPPluginTemplate.vstemplate
   - PluginMain.cs
   - WebRoot/ (entire folder)

4. Restart Visual Studio

5. Create New Project → Search for "AMP Plugin (.NET 8)"

## For Command Line / VS Code Users

### 1. Install .NET 8 SDK
```bash
# Ubuntu/Debian
sudo apt-get install -y dotnet-sdk-8.0

# Windows
winget install Microsoft.DotNet.SDK.8

# macOS
brew install dotnet
```

### 2. Clone Template
```bash
git clone [template-repository-url]
cd amp-plugin-template-net8
```

### 3. Customize Your Plugin

**Replace placeholders:**
```bash
# Plugin name
find . -type f -exec sed -i 's/\$safeprojectname\$/YourPluginName/g' {} \;
find . -type f -exec sed -i 's/\$projectname\$/Your Plugin Name/g' {} \;

# Author info
find . -type f -exec sed -i 's/\$author\$/Your Name/g' {} \;
find . -type f -exec sed -i 's/\$company\$/Your Company/g' {} \;
find . -type f -exec sed -i 's/\$year\$/2025/g' {} \;
```

**Linux/Mac alternative:**
```bash
# Use perl for in-place editing
find . -type f -exec perl -pi -e 's/\$safeprojectname\$/YourPluginName/g' {} \;
```

### 4. Generate Plugin GUID
```bash
# PowerShell
[guid]::NewGuid()

# Linux/Mac
uuidgen
```

Update in `PluginMain.cs`:
```csharp
public override Guid PluginID => new Guid("YOUR-NEW-GUID-HERE");
```

### 5. Set AMP Installation Path

**Windows:**
```cmd
setx AMPINSTALLDIR "C:\Program Files\CubeCoders\AMP"
```

**Linux:**
```bash
export AMPINSTALLDIR="/home/amp/.ampdata/instances/ADS01"
echo 'export AMPINSTALLDIR="/home/amp/.ampdata/instances/ADS01"' >> ~/.bashrc
```

**Or edit .csproj directly:**
```xml
<Reference Include="ModuleShared">
  <HintPath>C:\Your\Path\To\ModuleShared.dll</HintPath>
  <Private>false</Private>
</Reference>
```

### 6. Build Plugin
```bash
dotnet build -c Release
```

Output location: `bin/Release/net8.0/`

### 7. Deploy to AMP

**Windows:**
```cmd
xcopy /E /I bin\Release\net8.0\* "%ProgramData%\CubeCoders\AMP\instances\YourInstance\Plugins\YourPlugin\"
```

**Linux:**
```bash
sudo -u amp cp -r bin/Release/net8.0/* /home/amp/.ampdata/instances/ADS01/Plugins/YourPlugin/
```

### 8. Restart AMP
```bash
ampinstmgr restart [InstanceName]
```

## Verification

### Check Plugin Loaded
Look for your plugin in AMP logs:
```bash
# Linux
sudo -u amp tail -f /home/amp/.ampdata/instances/ADS01/AMP_Logs/*.log | grep "YourPlugin"

# Windows
Get-Content "$env:ProgramData\CubeCoders\AMP\instances\YourInstance\AMP_Logs\*.log" -Wait | Select-String "YourPlugin"
```

### Test Web Interface
Navigate to:
```
http://your-amp-server:port/YourPlugin/Plugin.html
```

### Test API Endpoint
```bash
curl -X POST http://your-amp-server:port/API/YourPlugin/ExampleMethod \
  -H "Content-Type: application/json" \
  -H "Accept: application/json" \
  -d '{}'
```

## Quick Start Script

### Windows (PowerShell)
```powershell
# quick-start.ps1
param(
    [string]$PluginName = "MyAMPPlugin",
    [string]$Author = "Your Name",
    [string]$Company = "Your Company"
)

# Clone template
git clone [template-url] $PluginName
cd $PluginName

# Replace placeholders
Get-ChildItem -Recurse -File | ForEach-Object {
    (Get-Content $_.FullName) `
        -replace '\$safeprojectname\$', $PluginName `
        -replace '\$projectname\$', $PluginName `
        -replace '\$author\$', $Author `
        -replace '\$company\$', $Company `
        -replace '\$year\$', (Get-Date).Year |
    Set-Content $_.FullName
}

# Generate GUID
$guid = [guid]::NewGuid().ToString()
Write-Host "Generated Plugin GUID: $guid"
Write-Host "Update PluginID in PluginMain.cs with this GUID"

# Rename files
Rename-Item "AMPPluginTemplate.csproj" "$PluginName.csproj"

Write-Host "Template customized! Next steps:"
Write-Host "1. Update PluginID GUID in PluginMain.cs"
Write-Host "2. Set AMPINSTALLDIR environment variable"
Write-Host "3. Run: dotnet build -c Release"
```

### Linux (Bash)
```bash
#!/bin/bash
# quick-start.sh

PLUGIN_NAME="${1:-MyAMPPlugin}"
AUTHOR="${2:-Your Name}"
COMPANY="${3:-Your Company}"

# Clone template
git clone [template-url] "$PLUGIN_NAME"
cd "$PLUGIN_NAME"

# Replace placeholders
find . -type f -not -path "*/\.*" -exec sed -i \
    -e "s/\$safeprojectname\$/$PLUGIN_NAME/g" \
    -e "s/\$projectname\$/$PLUGIN_NAME/g" \
    -e "s/\$author\$/$AUTHOR/g" \
    -e "s/\$company\$/$COMPANY/g" \
    -e "s/\$year\$/$(date +%Y)/g" \
    {} \;

# Generate GUID
GUID=$(uuidgen)
echo "Generated Plugin GUID: $GUID"
echo "Update PluginID in PluginMain.cs with this GUID"

# Rename files
mv AMPPluginTemplate.csproj "${PLUGIN_NAME}.csproj"

echo "Template customized! Next steps:"
echo "1. Update PluginID GUID in PluginMain.cs"
echo "2. Set AMPINSTALLDIR environment variable or edit .csproj"
echo "3. Run: dotnet build -c Release"
```

Usage:
```bash
./quick-start.sh MyPluginName "John Doe" "My Company"
```

## Troubleshooting

### Cannot Find ModuleShared.dll
**Solution:** Set `AMPINSTALLDIR` environment variable or update path in `.csproj`

### Plugin Not Loading
1. Check DLL is in correct location
2. Verify directory name matches DLL name
3. Check AMP logs for errors
4. Ensure `PluginMain` class exists

### Web Interface 404
1. Verify `HasWebRoot = true`
2. Check WebRoot folder deployed
3. Confirm plugin loaded successfully

### API Methods Not Working
1. Check `[WebMethod]` attribute present
2. Verify method is public
3. Test endpoint URL directly
4. Check CORS/authentication

## Development Workflow

### 1. Edit Code
Make changes to `PluginMain.cs` or web files

### 2. Build
```bash
dotnet build -c Release
```

### 3. Deploy
```bash
# Quick deploy script
./deploy.sh
```

### 4. Restart AMP
```bash
ampinstmgr restart [InstanceName]
```

### 5. Test
- Check logs for errors
- Test web interface
- Verify API methods

## IDE Setup

### Visual Studio 2022
- Install .NET 8 SDK
- Open `.csproj` file
- Set AMPINSTALLDIR
- Build solution (Ctrl+Shift+B)

### VS Code
- Install C# extension
- Install .NET 8 SDK
- Open folder
- Terminal → `dotnet build`

### JetBrains Rider
- Open `.csproj`
- Configure .NET 8 SDK
- Build solution

## Next Steps

1. Read `README.md` for detailed documentation
2. Explore example code in `PluginMain.cs`
3. Customize web interface in `WebRoot/`
4. Review AMP documentation for API references
5. Join AMP Discord for community support

## Support

- **Documentation:** See README.md
- **AMP Docs:** https://cubecoders.com/AMPdocs
- **Support:** https://cubecoders.com/support
- **Community:** AMP Discord

---

Happy plugin developing! 🚀

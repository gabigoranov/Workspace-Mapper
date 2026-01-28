using System;
using System.IO;

namespace WorkflowManager.Services.Startup;

public class MacStartupService : IStartupService
{
    private const string PlistName = "com.workflowmanager.app.plist";

    private static string LaunchAgentsPath =>
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            "Library", "LaunchAgents");

    private static string PlistPath =>
        Path.Combine(LaunchAgentsPath, PlistName);

    public bool IsEnabled()
    {
        if (!OperatingSystem.IsMacOS())
            throw new ArgumentException("Tried to check macOS startup on a non-macOS os!");

        return File.Exists(PlistPath);
    }

    public void Enable()
    {
        if (!OperatingSystem.IsMacOS())
            throw new ArgumentException("Tried to add app to macOS startup on a non-macOS os!");

        Directory.CreateDirectory(LaunchAgentsPath);

        var exePath = Environment.ProcessPath!;

        var plist = $"""
                     <?xml version="1.0" encoding="UTF-8"?>
                     <!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN"
                      "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
                     <plist version="1.0">
                     <dict>
                         <key>Label</key>
                         <string>com.workflowmanager.app</string>

                         <key>ProgramArguments</key>
                         <array>
                             <string>{exePath}</string>
                         </array>

                         <key>RunAtLoad</key>
                         <true/>
                     </dict>
                     </plist>
                     """;

        File.WriteAllText(PlistPath, plist);
    }

    public void Disable()
    {
        if (!OperatingSystem.IsMacOS())
            throw new ArgumentException("Tried to disable macOS startup on a non-macOS os!");

        if (File.Exists(PlistPath))
            File.Delete(PlistPath);
    }
}
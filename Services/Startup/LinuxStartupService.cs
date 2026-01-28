using System;
using System.IO;

namespace WorkflowManager.Services.Startup;

public class LinuxStartupService : IStartupService
{
    private const string DesktopFileName = "workflowmanager.desktop";

    private static string AutostartDir =>
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            ".config", "autostart");

    private static string DesktopFilePath =>
        Path.Combine(AutostartDir, DesktopFileName);

    public bool IsEnabled()
    {
        if (!OperatingSystem.IsLinux())
            throw new ArgumentException("Tried to check Linux startup on a non-linux os!");

        return File.Exists(DesktopFilePath);
    }

    public void Enable()
    {
        if (!OperatingSystem.IsLinux())
            throw new ArgumentException("Tried to add app to Linux startup on a non-linux os!");

        Directory.CreateDirectory(AutostartDir);

        var exePath = Environment.ProcessPath!;

        var desktopFile = $"""
                           [Desktop Entry]
                           Type=Application
                           Name=Workflow Manager
                           Exec={exePath}
                           X-GNOME-Autostart-enabled=true
                           """;

        File.WriteAllText(DesktopFilePath, desktopFile);
    }

    public void Disable()
    {
        if (!OperatingSystem.IsLinux())
            throw new ArgumentException("Tried to disable Linux startup on a non-linux os!");

        if (File.Exists(DesktopFilePath))
            File.Delete(DesktopFilePath);
    }
}
using System;
using Microsoft.Win32;

namespace WorkflowManager.Services.Startup;

public class WindowsStartupService : IStartupService
{
    public bool IsEnabled()
    {
        if (!OperatingSystem.IsWindows())
            throw new ArgumentException("Tried to check windows app startup on a non-windows os!");
        
        using var key = Registry.CurrentUser.OpenSubKey(
            @"Software\Microsoft\Windows\CurrentVersion\Run", true);

        return key?.GetValue("Workflow Manager") != null;
    }

    public void Enable()
    {
        if (!OperatingSystem.IsWindows())
            throw new ArgumentException("Tried to add app to windows startup apps on a non-windows os!");
        
        using var key = Registry.CurrentUser.OpenSubKey(
            @"Software\Microsoft\Windows\CurrentVersion\Run", true);

        key?.SetValue("Workflow Manager", Environment.ProcessPath!);
    }

    public void Disable()
    {
        if (!OperatingSystem.IsWindows())
            throw new ArgumentException("Tried to disable app from windows startup apps on a non-windows os!");
        
        using var key = Registry.CurrentUser.OpenSubKey(
            @"Software\Microsoft\Windows\CurrentVersion\Run", true);

        key?.DeleteValue("Workflow Manager", false);
    }
}
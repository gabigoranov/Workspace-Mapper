
using System;
using System.Linq;
using Avalonia.Controls;
using System.Threading.Tasks;

namespace WorkflowManager.Services.Dialog;

// Instead of injecting a Window directly, inject a function that returns the main window
public class DialogService(Func<Window> getWindow) : IDialogService
{
    /// <inheritdoc/>
    public async Task<string?> SelectFolderAsync()
    {
        var dialog = new OpenFolderDialog { Title = "Select a folder" };
        return await dialog.ShowAsync(getWindow());
    }
}

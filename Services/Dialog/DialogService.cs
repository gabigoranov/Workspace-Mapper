
using System;
using System.Linq;
using Avalonia.Controls;
using System.Threading.Tasks;

namespace WorkflowManager.Services.Dialog;

public class DialogService : IDialogService
{
    private readonly Func<Window> _getWindow;

    // Instead of injecting a Window directly, inject a function that returns the main window
    public DialogService(Func<Window> getWindow)
    {
        _getWindow = getWindow;
    }

    public async Task<string?> SelectFolderAsync()
    {
        var dialog = new OpenFolderDialog { Title = "Select a folder" };
        return await dialog.ShowAsync(_getWindow());
    }

    public async Task<string[]?> SelectFilesAsync(string? filterName = null, string[]? extensions = null)
    {
        var dialog = new OpenFileDialog();
        if (filterName != null && extensions != null)
        {
            dialog.Filters.Add(new FileDialogFilter { Name = filterName, Extensions = extensions.ToList() });
        }
        return await dialog.ShowAsync(_getWindow());
    }
}

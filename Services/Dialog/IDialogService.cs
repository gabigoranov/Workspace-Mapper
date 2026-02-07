using System.Threading.Tasks;

namespace WorkflowManager.Services.Dialog;

public interface IDialogService
{
    /// <summary>
    /// Opens a dialog window to select a folder/directory.
    /// </summary>
    /// <returns>The directory path represented as string, or null if canceled.</returns>
    Task<string?> SelectFolderAsync();
}

using System.Threading.Tasks;

namespace WorkflowManager.Services.Dialog;

public interface IDialogService
{
    Task<string?> SelectFolderAsync();
    Task<string[]?> SelectFilesAsync(string? filterName = null, string[]? extensions = null);
}

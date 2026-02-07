using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkflowManager.Services.Dialog;

namespace WorkflowManager.ViewModels.Binding;

/// <summary>
/// A Binding model that expands upon the ProcessBindingModel.
/// </summary>
public partial class CommandProcessBindingModel : ProcessBindingModel
{
    [ObservableProperty] 
    [Required(ErrorMessage = "A directory is required")]
    private string _directory;

    [ObservableProperty] 
    [Required(ErrorMessage = "A command is required")]
    private string _command;
}
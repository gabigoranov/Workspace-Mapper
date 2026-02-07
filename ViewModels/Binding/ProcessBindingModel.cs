using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkflowManager.Models.Common;

namespace WorkflowManager.ViewModels.Binding;

/// <summary>
/// A binding model used for forms.
/// Implements the base Process data and expects to be inherited.
/// </summary>
public partial class ProcessBindingModel : ObservableValidator
{
    public int Id { get; set; }

    [ObservableProperty]
    [Required(ErrorMessage = "A title is required")]
    [MaxLength(30, ErrorMessage = "Title cannot exceed 30 characters")]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _icon = "Power";

    [ObservableProperty]
    private bool _isFullscreen = false;

    [ObservableProperty]
    private string _monitor = "1";

    [ObservableProperty]
    private string _position = "100,100";

    [ObservableProperty]
    private string _size = "800x450";

    [ObservableProperty] 
    private ProcessType _discriminator = ProcessType.CommandProcess;

    // Expose the ObservableValidator Validate method
    public bool Validate()
    {
        ValidateAllProperties();
        return !HasErrors;
    }
}
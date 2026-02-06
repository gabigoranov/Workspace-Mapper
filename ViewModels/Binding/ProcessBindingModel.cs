using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WorkflowManager.ViewModels.Binding;

public partial class ProcessBindingModel : ObservableValidator
{
    private int _id;
    public int Id 
    { 
        get => _id; 
        set => SetProperty(ref _id, value); 
    }

    [ObservableProperty]
    [Required(ErrorMessage = "A title is required")]
    [MaxLength(30, ErrorMessage = "Title cannot exceed 30 characters")]
    private string _title = string.Empty;

    [ObservableProperty]
    private string? _icon;

    [ObservableProperty]
    private bool _isFullscreen;

    [ObservableProperty]
    private string? _monitor;

    [ObservableProperty]
    private string? _position;

    [ObservableProperty]
    private string? _size;

    [ObservableProperty]
    private string? _directory;

    [ObservableProperty]
    private string? _command;

    public bool Validate()
    {
        ValidateAllProperties();
        return !HasErrors;
    }
}
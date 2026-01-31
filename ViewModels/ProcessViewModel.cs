using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkflowManager.Models;

namespace WorkflowManager.ViewModels;

public partial class ProcessViewModel : ObservableValidator
{
    // Fill out as many processes to be added by filling out one sub form at a time before adding the process to the collection
    
    [ObservableProperty]
    [MaxLength(30, ErrorMessage = "Process name must be st most 30 characters")]
    private string _title  = string.Empty;
    
    [ObservableProperty]
    [MaxLength(255, ErrorMessage = "Process name must be 255 characters")]
    private string _directory  = string.Empty;
    
    [ObservableProperty]
    [MaxLength(255, ErrorMessage = "Process name must be 255 characters")]
    private string _command  = string.Empty;
    
    [ObservableProperty] 
    private bool _isEditing = false;
    
    public bool Validate()
    {
        ValidateAllProperties();
        return !HasErrors;
    }
    
    public ProcessViewModel() { }

    public ProcessViewModel(Process process)
    {
        _title = process.Title;
        _directory = process.Directory;
        _command = process.Command;
        _isEditing = true;
    }

    public Process ToProcess() => new Process
    {
        Title = Title,
        Directory = Directory,
        Command = Command
    };

    public void ApplyToProcess(Process process)
    {
        process.Title = Title;
        process.Directory = Directory;
        process.Command = Command;
    }
}
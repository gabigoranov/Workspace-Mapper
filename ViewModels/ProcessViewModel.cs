using CommunityToolkit.Mvvm.ComponentModel;
using WorkflowManager.ViewModels.Binding;

namespace WorkflowManager.ViewModels;

public partial class ProcessViewModel : ObservableValidator
{
    [ObservableProperty] 
    private ProcessBindingModel _bindingModel =  new ProcessBindingModel();

    // --- STATE ---
    [ObservableProperty] 
    private bool _isEditing = false;

    public ProcessViewModel() { }

    public ProcessViewModel(ProcessBindingModel process)
    {
        BindingModel = process;
        
        IsEditing = true;
    }
}
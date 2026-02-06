using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkflowManager.Models;
using WorkflowManager.Services.Common.Navigation;
using WorkflowManager.Services.Common.Workflow;
using WorkflowManager.Services.Dialog;
using WorkflowManager.ViewModels.Binding;
using Process = WorkflowManager.Models.Common.Process;
namespace WorkflowManager.ViewModels;

public partial class CreateWorkflowViewModel(IWorkflowService workflowService, INavigationService navigation, IDialogService dialogService, IMapper mapper): ViewModelBase
{
    [ObservableProperty]
    [Required(ErrorMessage = "Workflow name is required")]
    [MaxLength(30, ErrorMessage = "Workflow name must be at most 30 characters")]
    private string _workflowTitle = string.Empty;
    
    [ObservableProperty]
    private ObservableCollection<Process> _workflowProcesses = [];
    
    [ObservableProperty]
    private ProcessViewModel _currentProcessVm = new();

    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(WorkflowProcesses))]
    private Process? _editingProcess;
    
    [RelayCommand]
    private async Task CreateWorkflow()
    {
        ValidateAllProperties();
        if (HasErrors) return;
        
        Workflow workflow = new Workflow{Title = WorkflowTitle, Processes = WorkflowProcesses.ToList()};
        await workflowService.CreateWorkflowAsync(workflow);
        navigation.Navigate<HomeViewModel>();
    }

    [RelayCommand]
    private async Task ChooseProcessDirectory()
    {
        var result = await dialogService.SelectFolderAsync();
        /*if (!string.IsNullOrEmpty(result))
            CurrentProcessVm.Directory = result;*/
    }
    
    
    [RelayCommand]
    private void StartEditProcess(Process process)
    {
        EditingProcess = process;
        CurrentProcessVm.IsEditing = true;

        CurrentProcessVm = new ProcessViewModel(mapper.Map<ProcessBindingModel>(process));
    }

    [RelayCommand]
    private void AddOrEditProcess()
    {
        if (!CurrentProcessVm.BindingModel.Validate()) return;

        if (CurrentProcessVm.IsEditing && EditingProcess != null)
        {
            mapper.Map(EditingProcess, CurrentProcessVm.BindingModel);
            EditingProcess = null;
            CurrentProcessVm.IsEditing = false;
        }
        else
        {
            WorkflowProcesses.Add(mapper.Map<Process>(CurrentProcessVm.BindingModel));
        }

        CurrentProcessVm = new ProcessViewModel();
    }

    [RelayCommand]
    private void DeleteProcess(Process process)
    {
        if (EditingProcess?.Id == process.Id)
        {
            EditingProcess = null;
            CurrentProcessVm.IsEditing = false;
        }

        WorkflowProcesses.Remove(process);
    }
}
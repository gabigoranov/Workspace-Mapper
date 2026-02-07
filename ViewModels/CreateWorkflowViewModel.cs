using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkflowManager.Models;
using WorkflowManager.Models.Common;
using WorkflowManager.Services.Dialog;
using WorkflowManager.Services.Navigation;
using WorkflowManager.Services.Workflow;
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
    private ObservableCollection<ProcessBindingModel> _workflowProcesses = [];
    
    [ObservableProperty]
    private ProcessViewModel _currentProcessVm = new();

    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(WorkflowProcesses))]
    private ProcessBindingModel? _editingProcess;
    
    // Expose the Enum values to the UI
    public IEnumerable<ProcessType> ProcessTypes => Enum.GetValues<ProcessType>();

    private readonly Dictionary<ProcessType, ObservableValidator> _processTypeViewModels = new Dictionary<ProcessType, ObservableValidator>()
    {
        {ProcessType.CommandProcess, new CommandProcessBindingModel()}
    };

    public ObservableValidator SelectedProcessTypeVm =>
        _processTypeViewModels[CurrentProcessVm.BindingModel.Discriminator];
    
    
    [RelayCommand]
    private async Task CreateWorkflow()
    {
        ValidateAllProperties();
        if (HasErrors) return;
        
        Workflow workflow = new Workflow{Title = WorkflowTitle, Processes = mapper.Map<List<Process>>(WorkflowProcesses)};
        await workflowService.CreateWorkflowAsync(workflow);
        navigation.Navigate<HomeViewModel>();
    }

    [RelayCommand]
    private void StartEditProcess(ProcessBindingModel process)
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
            switch (CurrentProcessVm.BindingModel.Discriminator)
            {
                case ProcessType.CommandProcess:
                    CommandProcessBindingModel model = (CommandProcessBindingModel)SelectedProcessTypeVm;
                    mapper.Map(CurrentProcessVm.BindingModel, model);
                    WorkflowProcesses.Add(model);
                    break;
            }
        }

        CurrentProcessVm = new ProcessViewModel();
    }

    [RelayCommand]
    private void DeleteProcess(ProcessBindingModel process)
    {
        if (EditingProcess?.Id == process.Id)
        {
            EditingProcess = null;
            CurrentProcessVm.IsEditing = false;
        }

        WorkflowProcesses.Remove(process);
    }
}
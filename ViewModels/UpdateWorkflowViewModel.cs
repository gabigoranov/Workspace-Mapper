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
using WorkflowManager.Services.Common.Navigation;
using WorkflowManager.Services.Common.Workflow;
using WorkflowManager.Services.Dialog;
using WorkflowManager.Services.Process;
using WorkflowManager.Services.WorkflowState;
using WorkflowManager.ViewModels.Binding;
using Process = WorkflowManager.Models.Common.Process;

namespace WorkflowManager.ViewModels;

public partial class UpdateWorkflowViewModel(
    IWorkflowService workflowService,
    IProcessService processService,
    INavigationService navigation,
    IWorkflowStateService workflowStateService,
    IDialogService dialogService,
    IMapper mapper) : ViewModelBase
{
    private readonly int _workflowId = workflowStateService.SelectedWorkflow?.Id ?? throw new InvalidOperationException("No workflow selected for editing.");

    [ObservableProperty]
    [Required(ErrorMessage = "Workflow name is required")]
    [MaxLength(30, ErrorMessage = "Workflow name must be at most 30 characters")]
    private string _workflowTitle = workflowStateService.SelectedWorkflow.Title;

    [ObservableProperty]
    private ObservableCollection<Process> _workflowProcesses =
        new(workflowStateService.SelectedWorkflow!.Processes);

    [ObservableProperty]
    private ProcessViewModel _currentProcessVm = new();

    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(WorkflowProcesses))]
    private Process? _editingProcess;
    
    
    private readonly List<Process> _deletedProcesses = new();

    [RelayCommand]
    private async Task ChooseProcessDirectory()
    {
        var result = await dialogService.SelectFolderAsync();
        /*if (!string.IsNullOrEmpty(result))
            CurrentProcessVm.Directory = result;*/
    }
    
    [RelayCommand]
    private async Task UpdateWorkflow()
    {
        ValidateAllProperties();
        if (HasErrors) return;

        // First, delete removed processes
        foreach (var deleted in _deletedProcesses)
        {
            await processService.DeleteProcessAsync(deleted.Id);
        }
        _deletedProcesses.Clear();

        // Then update workflow with remaining processes
        Workflow updatedWorkflow = new()
        {
            Id = _workflowId,
            Title = WorkflowTitle,
            Processes = WorkflowProcesses.ToList()
        };

        await workflowService.UpdateWorkflowAsync(_workflowId, updatedWorkflow);
        workflowStateService.SelectedWorkflow = null; // reset to be safe
        navigation.Navigate<HomeViewModel>();
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
    
        // Only track if it has an ID (i.e., exists in DB)
        if (process.Id != 0)
            _deletedProcesses.Add(process);
    }

}

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkflowManager.Models;
using WorkflowManager.Services.Common.Navigation;
using WorkflowManager.Services.Common.Workflow;
using WorkflowManager.Services.Dialog;
using WorkflowManager.Services.Process;
using WorkflowManager.Services.WorkflowState;
using Process = WorkflowManager.Models.Process;

namespace WorkflowManager.ViewModels;

public partial class UpdateWorkflowViewModel(
    IWorkflowService workflowService,
    IProcessService processService,
    INavigationService navigation,
    IWorkflowStateService workflowStateService,
    IDialogService dialogService) : ViewModelBase
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
    [MaxLength(30, ErrorMessage = "Process name must be at most 30 characters")]
    private string _processTitle = string.Empty;

    [ObservableProperty]
    [MaxLength(255, ErrorMessage = "Process directory must be at most 255 characters")]
    private string _processDirectory = string.Empty;

    [ObservableProperty]
    [MaxLength(255, ErrorMessage = "Process command must be at most 255 characters")]
    private string _processCommand = string.Empty;

    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(WorkflowProcesses))]
    private Process? _editingProcess;
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(EditProcessCommand))]
    private bool _isEditingProcess = false;

    [RelayCommand]
    private async Task ChooseProcessDirectory()
    {
        var result = await dialogService.SelectFolderAsync();
        if (!string.IsNullOrEmpty(result))
            ProcessDirectory = result;
    }
    
    [RelayCommand]
    private async Task UpdateWorkflow()
    {
        ValidateAllProperties();
        if (HasErrors) return;

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
    private void AddProcess()
    {
        ValidateAllProperties();
        if (HasErrors) return;

        WorkflowProcesses.Add(new Process
        {
            Title = ProcessTitle,
            Directory = ProcessDirectory,
            Command = ProcessCommand
        });

        ProcessTitle = string.Empty;
        ProcessDirectory = string.Empty;
        ProcessCommand = string.Empty;

        ClearErrors(nameof(ProcessTitle));
        ClearErrors(nameof(ProcessDirectory));
        ClearErrors(nameof(ProcessCommand));
    }

    [RelayCommand]
    private void StartEditProcess(Process process)
    {
        EditingProcess = process;
        IsEditingProcess = true;

        ProcessTitle = process.Title;
        ProcessDirectory = process.Directory;
        ProcessCommand = process.Command;
    }

    [RelayCommand(CanExecute = nameof(IsEditingProcess))]
    private void EditProcess()
    {
        // Apply form fields to selected process.
        EditingProcess!.Title = ProcessTitle;
        EditingProcess.Directory = ProcessDirectory;
        EditingProcess.Command = ProcessCommand;
        
        EditingProcess = null;
        IsEditingProcess = false;
    }
}

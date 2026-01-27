using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkflowManager.Models;
using WorkflowManager.Services.Common.Navigation;
using WorkflowManager.Services.Common.Workflow;
using WorkflowManager.Services.Dialog;
using Process = WorkflowManager.Models.Process;
namespace WorkflowManager.ViewModels;

public partial class CreateWorkflowViewModel(IWorkflowService workflowService, INavigationService navigation, IDialogService dialogService): ViewModelBase
{
    [ObservableProperty]
    [Required(ErrorMessage = "Workflow name is required")]
    [MaxLength(30, ErrorMessage = "Workflow name must be at most 30 characters")]
    private string _workflowTitle = string.Empty;
    
    [ObservableProperty]
    private ObservableCollection<Process> _workflowProcesses = [];

    // Fill out as many processes to be added by filling out one sub form at a time before adding the process to the collection
    
    [ObservableProperty]
    [MaxLength(30, ErrorMessage = "Process name must be st most 30 characters")]
    private string _processTitle  = string.Empty;
    
    [ObservableProperty]
    [MaxLength(255, ErrorMessage = "Process name must be 255 characters")]
    private string _processDirectory  = string.Empty;
    
    [ObservableProperty]
    [MaxLength(255, ErrorMessage = "Process name must be 255 characters")]
    private string _processCommand  = string.Empty;
    
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
        if (!string.IsNullOrEmpty(result))
            ProcessDirectory = result;
    }

    
    [RelayCommand]
    private void AddProcess()
    {
        // Validate only the Process fields
        ValidateAllProperties();
        if (HasErrors) return;

        WorkflowProcesses.Add(new Process { 
            Title = ProcessTitle, 
            Directory = ProcessDirectory, 
            Command = ProcessCommand 
        });

        // Clear the sub-form for the next process
        ProcessTitle = string.Empty;
        ProcessDirectory = string.Empty;
        ProcessCommand = string.Empty;
    
        // Clear validation errors for the next entry
        ClearErrors(nameof(ProcessTitle));
        ClearErrors(nameof(ProcessDirectory));
        ClearErrors(nameof(ProcessCommand));
    }
}
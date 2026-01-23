using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkflowManager.Models;
using WorkflowManager.Services.Common.Navigation;
using WorkflowManager.Services.Common.Workflow;
using WorkflowManager.Services.Process;
using WorkflowManager.Services.WorkflowState;

namespace WorkflowManager.ViewModels;

public partial class HomeViewModel(IWorkflowService workflowService, INavigationService navigation, IProcessService processService, IWorkflowStateService workflowState)
    : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<Workflow> _workflows = new(workflowService.GetAllWorkflows());
    
    [ObservableProperty]
    private Workflow? _selectedWorkflow = workflowState.SelectedWorkflow;

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(StartWorkflowCommand))]
    private bool _isExecutingWorkflow;
    
    // This partial method runs every time SelectedWorkflow changes
    partial void OnSelectedWorkflowChanged(Workflow? value)
    {
        // Tell the commands to re-check the 'CanInteractWithWorkflow' logic
        StartWorkflowCommand.NotifyCanExecuteChanged();
        EditWorkflowCommand.NotifyCanExecuteChanged();
        DeleteWorkflowCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    private void GoCreateWorkflow()
    {
        navigation.Navigate<CreateWorkflowViewModel>();
    }
    
    [RelayCommand(CanExecute = nameof(CanInteractWithWorkflow))]
    private void EditWorkflow()
    {
        workflowState.SelectedWorkflow = SelectedWorkflow;
        navigation.Navigate<UpdateWorkflowViewModel>();
    }
    
    [RelayCommand(CanExecute = nameof(CanInteractWithWorkflow))]
    private async Task DeleteWorkflow()
    {
        if (SelectedWorkflow == null)
            return;

        try
        {
            await workflowService.DeleteWorkflowAsync(SelectedWorkflow.Id);
            Workflows.Remove(SelectedWorkflow);
        }
        finally 
        {
            IsExecutingWorkflow = false;
            SelectedWorkflow = null; 
        }
    }

    [RelayCommand(CanExecute = nameof(CanInteractWithWorkflow))]
    private async Task StartWorkflow()
    {
        if (SelectedWorkflow == null || IsExecutingWorkflow)
            return;

        try 
        {
            IsExecutingWorkflow = true;
        
            foreach (var step in SelectedWorkflow.Processes)
            {
                // Call your new service here
                await processService.ExecuteProcessAsync(step);
            
                // TODO: implement some sort of warning in case of process failure.
            }

            var updated = await workflowService.UpdateWorkflowLastStartupAsync(SelectedWorkflow.Id);

            // Find the index of the old item
            var index = Workflows.IndexOf(Workflows.First(w => w.Id == updated.Id));
        
            if (index != -1)
            {
                // Replacing the item at the index triggers the CollectionChanged event
                Workflows[index] = updated; 
            }
        }
        finally 
        {
            IsExecutingWorkflow = false;
            SelectedWorkflow = null; // unselect right after startup
        }
    }
    
    // This method dictates if the commands are active
    private bool CanInteractWithWorkflow() => SelectedWorkflow != null;
}
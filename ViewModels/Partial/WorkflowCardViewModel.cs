using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkflowManager.Models;
using WorkflowManager.Services.Navigation;
using WorkflowManager.Services.Process;
using WorkflowManager.Services.Workflow;
using WorkflowManager.Services.WorkflowState;

namespace WorkflowManager.ViewModels.Partial;

public partial class WorkflowCardViewModel(
    Workflow workflow,
    IWorkflowService workflowService,
    IWorkflowStateService workflowState,
    IProcessService processService,
    INavigationService navigation,
    Action<WorkflowCardViewModel> onDeleteRequested)
    : ObservableObject
{
    [ObservableProperty] private Workflow _workflow = workflow;
    [ObservableProperty] private bool _isExecutingWorkflow;

    [RelayCommand]
    private void EditWorkflow()
    {
        workflowState.SelectedWorkflow = Workflow;
        navigation.Navigate<UpdateWorkflowViewModel>();
    }

    [RelayCommand]
    private async Task DeleteWorkflow()
    {
        try
        {
            IsExecutingWorkflow = true;
            await workflowService.DeleteWorkflowAsync(Workflow.Id);
            onDeleteRequested?.Invoke(this);
        }
        finally
        {
            IsExecutingWorkflow = false;
        }
    }

    [RelayCommand]
    private async Task StartWorkflow()
    {
        if (IsExecutingWorkflow) return;

        try
        {
            IsExecutingWorkflow = true;

            foreach (var step in Workflow.Processes)
            {
                await step.Execute();
            }

            Workflow = await workflowService.UpdateWorkflowLastStartupAsync(Workflow.Id);
        }
        finally
        {
            IsExecutingWorkflow = false;
        }
    }
}
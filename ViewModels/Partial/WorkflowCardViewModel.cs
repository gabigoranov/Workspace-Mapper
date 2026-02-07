using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkflowManager.Models;
using WorkflowManager.Services.Navigation;
using WorkflowManager.Services.Workflow;
using WorkflowManager.Services.WorkflowState;

namespace WorkflowManager.ViewModels.Partial;

public partial class WorkflowCardViewModel(
    Workflow workflow,
    IWorkflowService workflowService,
    IWorkflowStateService workflowState,
    INavigationService navigation,
    Action<WorkflowCardViewModel> onDeleteRequested)
    : ObservableObject
{
    [ObservableProperty] private Workflow _workflow = workflow;
    [ObservableProperty] private bool _isExecutingWorkflow;

    /// <summary>
    /// Navigates to edit workflow view
    /// </summary>
    [RelayCommand]
    private void EditWorkflow()
    {
        workflowState.SelectedWorkflow = Workflow;
        navigation.Navigate<WorkflowEditorViewModel>();
    }

    [RelayCommand]
    private async Task DeleteWorkflow()
    {
        await workflowService.DeleteWorkflowAsync(Workflow.Id);
        onDeleteRequested.Invoke(this);
    }

    /// <summary>
    /// Executes each process in the workflow
    /// </summary>
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
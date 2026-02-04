using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkflowManager.Models;
using WorkflowManager.Services.Common.Navigation;
using WorkflowManager.Services.Common.Workflow;
using WorkflowManager.Services.Process;
using WorkflowManager.Services.WorkflowState;

namespace WorkflowManager.ViewModels.Partial;

public partial class WorkflowCardViewModel : ObservableObject
{
    private readonly IWorkflowService _workflowService;
    private readonly IWorkflowStateService _workflowState;
    private readonly IProcessService _processService;
    private readonly INavigationService _navigation;
    private readonly Action<WorkflowCardViewModel> _onDeleteRequested;

    [ObservableProperty] private Workflow _workflow;
    [ObservableProperty] private bool _isExecutingWorkflow;

    public WorkflowCardViewModel(
        Workflow workflow,
        IWorkflowService workflowService,
        IWorkflowStateService workflowState,
        IProcessService processService,
        INavigationService navigation,
        Action<WorkflowCardViewModel> onDeleteRequested)
    {
        _workflow = workflow;
        _workflowService = workflowService;
        _workflowState = workflowState;
        _processService = processService;
        _navigation = navigation;
        _onDeleteRequested = onDeleteRequested;
    }

    [RelayCommand]
    private void EditWorkflow()
    {
        _workflowState.SelectedWorkflow = Workflow;
        _navigation.Navigate<UpdateWorkflowViewModel>();
    }

    [RelayCommand]
    private async Task DeleteWorkflow()
    {
        try
        {
            IsExecutingWorkflow = true;
            await _workflowService.DeleteWorkflowAsync(Workflow.Id);
            _onDeleteRequested?.Invoke(this);
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
                await _processService.ExecuteProcessAsync(step);
            }

            Workflow = await _workflowService.UpdateWorkflowLastStartupAsync(Workflow.Id);
        }
        finally
        {
            IsExecutingWorkflow = false;
        }
    }
}
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkflowManager.Services.Common.Navigation;
using WorkflowManager.Services.Common.Workflow;
using WorkflowManager.Services.Process;
using WorkflowManager.Services.WorkflowState;

namespace WorkflowManager.ViewModels.Partial;

public partial class WorkflowListViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<WorkflowCardViewModel> _workflowCards;

    public WorkflowListViewModel(
        IWorkflowService workflowService, 
        IWorkflowStateService workflowState, 
        IProcessService processService, 
        INavigationService navigation)
    {
        var models = workflowService.GetAllWorkflows();
        
        var viewModels = models.Select(w => new WorkflowCardViewModel(
            w, 
            workflowService, 
            workflowState, 
            processService, 
            navigation,
            OnCardDeleted));

        _workflowCards = new ObservableCollection<WorkflowCardViewModel>(viewModels);
    }

    private void OnCardDeleted(WorkflowCardViewModel card)
    {
        WorkflowCards.Remove(card);
    }
}
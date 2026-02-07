using WorkflowManager.Models;

namespace WorkflowManager.Services.WorkflowState;

public class WorkflowStateService : IWorkflowStateService
{
    public Models.Workflow? SelectedWorkflow { get; set; }
}
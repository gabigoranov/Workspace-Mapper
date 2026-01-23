using WorkflowManager.Models;

namespace WorkflowManager.Services.WorkflowState;

public class WorkflowStateService : IWorkflowStateService
{
    public Workflow? SelectedWorkflow { get; set; }
}
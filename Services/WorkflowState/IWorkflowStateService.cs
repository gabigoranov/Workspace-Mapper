using WorkflowManager.Models;

namespace WorkflowManager.Services.WorkflowState;

public interface IWorkflowStateService
{
    Workflow? SelectedWorkflow  { get; set; }
}
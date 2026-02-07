
namespace WorkflowManager.Services.WorkflowState;

/// <summary>
/// A helper singleton for passing the needed workflow between views.
/// </summary>
public interface IWorkflowStateService
{
    Models.Workflow? SelectedWorkflow  { get; set; }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkflowManager.Services.Workflow;

public interface IWorkflowService
{
	/// <summary>
	/// Creates a workflow asynchronously.
	/// </summary>
	/// <param name="workflow">The model to use.</param>
	/// <returns>The created workflow with it's id.</returns>
    public Task<Models.Workflow> CreateWorkflowAsync(Models.Workflow workflow);
	
	/// <summary>
	/// Deletes a workflow by it's id.	
	/// </summary>
	/// <param name="workflowId">The id of the workflow.</param>
    public Task DeleteWorkflowAsync(int workflowId);

	/// <summary>
	/// Updates a workflow asynchronously.
	/// </summary>
	/// <param name="workflowId">The id of the workflow to be updated.</param>
	/// <param name="model">The model to use for the update.</param>
	/// <returns>The updated workflow.</returns>
	public Task<Models.Workflow> UpdateWorkflowAsync(int workflowId, Models.Workflow model);
	
	/// <summary>
	/// Returns all saved workflows.
	/// </summary>
	/// <returns>A list of workflows.</returns>
	public List<Models.Workflow> GetAllWorkflows();
	
	/// <summary>
	/// Updates a workflow's LastStartup property.
	/// </summary>
	/// <param name="workflowId">The id of the workflow to update.</param>
	/// <returns>The updated workflow.</returns>
	public Task<Models.Workflow> UpdateWorkflowLastStartupAsync(int workflowId);

}
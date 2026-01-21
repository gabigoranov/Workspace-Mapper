using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowManager.ViewModels;

namespace WorkflowManager.Services.Common.Workflow;

public interface IWorkflowService
{
    public Task<Models.Workflow> CreateWorkflowAsync(Models.Workflow workflow);
    public Task DeleteWorkflowAsync(int workflowId);
    public Task<Models.Workflow> UpdateWorkflowAsync(int workflowId, Models.Workflow model);
	public List<Models.Workflow> GetAllWorkflows();
	public Task<Models.Workflow> UpdateWorkflowLastStartupAsync(int workflowId);

}
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowManager.ViewModels;

namespace WorkflowManager.Services.Common.Workflow;

public interface IWorkflowService
{
    public Task<Models.Workflow> CreateWorkflowAsync(Models.Workflow workflow);
	public List<Models.Workflow> GetAllWorkflows();
	public Task<Models.Workflow> UpdateWorkflowLastStartupAsync(int workflowId);

}
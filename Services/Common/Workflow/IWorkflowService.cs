using System.Threading.Tasks;
using WorkflowManager.ViewModels;

namespace WorkflowManager.Services.Common.Workflow;

public interface IWorkflowService
{
    public Task<Models.Workflow> CreateWorkflowAsync(WorkflowViewModel workflow);
}
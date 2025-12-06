using System.Threading.Tasks;
using StudyPlatform.Data.Common;
using WorkflowManager.ViewModels;

namespace WorkflowManager.Services.Common.Workflow;

public class WorkflowService : IWorkflowService
{
    private readonly IRepository  _repository;

    public WorkflowService(IRepository repository)
    {
        _repository = repository;
    }

    public Task<Models.Workflow> CreateWorkflowAsync(WorkflowViewModel workflow)
    {
        throw new System.NotImplementedException();
    }
}
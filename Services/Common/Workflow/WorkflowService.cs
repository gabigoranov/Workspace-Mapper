using System.Diagnostics;
using System.Threading.Tasks;
using StudyPlatform.Data.Common;
using WorkflowManager.ViewModels;

namespace WorkflowManager.Services.Common.Workflow;

public class WorkflowService(IRepository repository) : IWorkflowService
{
    public async Task<Models.Workflow> CreateWorkflowAsync(Models.Workflow workflow)
    {
        await repository.AddAsync(workflow);
        await repository.SaveChangesAsync();
        
        return workflow;
    }
}
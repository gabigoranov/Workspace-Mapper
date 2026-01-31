using System.Threading.Tasks;

namespace WorkflowManager.Services.Process;

public interface IProcessService
{
    public Task ExecuteProcessAsync(Models.Process workflowProcess);
    public Task<Models.Process> EditProcessAsync(int id, Models.Process workflowProcess);
    public Task DeleteProcessAsync(int id);
}
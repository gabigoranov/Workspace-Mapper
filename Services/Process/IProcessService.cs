using System.Threading.Tasks;

namespace WorkflowManager.Services.Process;

public interface IProcessService
{
    public Task<Models.Common.Process> EditProcessAsync(int id, Models.Common.Process workflowProcess);
    public Task DeleteProcessAsync(int id);
}
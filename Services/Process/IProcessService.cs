using System.Threading.Tasks;

namespace WorkflowManager.Services.Process;

public interface IProcessService
{
    public Task ExecuteProcessAsync(Models.Process workflowProcess);
}
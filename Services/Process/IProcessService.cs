using System.Threading.Tasks;

namespace WorkflowManager.Services.Process;

public interface IProcessService
{
    /// <summary>
    /// Updates a process asynchronously.
    /// </summary>
    /// <param name="id">The id of the process.</param>
    /// <param name="workflowProcess">The model to use for updating.</param>
    /// <returns>The updated process.</returns>
    public Task<Models.Common.Process> UpdateProcessAsync(int id, Models.Common.Process workflowProcess);
    
    /// <summary>
    /// Deletes a process asynchronously.
    /// </summary>
    /// <param name="id">The id of the process.</param>
    /// <returns>Nothing.</returns>
    public Task DeleteProcessAsync(int id);
}
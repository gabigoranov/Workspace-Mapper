using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using StudyPlatform.Data.Common;

namespace WorkflowManager.Services.Process;

public class ProcessService(IRepository repository, IMapper mapper) : IProcessService
{
    public async Task<Models.Common.Process> EditProcessAsync(int id, Models.Common.Process workflowProcess)
    {
        var process = await repository.GetByIdAsync<Models.Common.Process>(id);
        if (process == null)
            throw new KeyNotFoundException("Workflow not found");

        mapper.Map(workflowProcess, process);

        await repository.SaveChangesAsync();
        return process;
    }

    public async Task DeleteProcessAsync(int id)
    {
        await repository.ExecuteDeleteAsync<Models.Common.Process>(x => x.Id == id);
        await repository.SaveChangesAsync();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudyPlatform.Data.Common;

namespace WorkflowManager.Services.Common.Workflow;

public class WorkflowService(IRepository repository) : IWorkflowService
{
    public async Task<Models.Workflow> CreateWorkflowAsync(Models.Workflow workflow)
    {
        await repository.AddAsync(workflow);
        await repository.SaveChangesAsync();
        
        return workflow;
    }

    public List<Models.Workflow> GetAllWorkflows()
    {
        return repository.AllReadonly<Models.Workflow>().Include(w => w.Processes).ToList();
    }

    public async Task<Models.Workflow> UpdateWorkflowLastStartupAsync(int workflowId)
    {
        var workflow = await repository.All<Models.Workflow>().SingleAsync(w => w.Id == workflowId);
        
        workflow.LastStartup = DateTime.Now;
        
        await repository.SaveChangesAsync();

        return workflow;
    }
}
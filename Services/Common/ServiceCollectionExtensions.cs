using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudyPlatform.Data.Common;
using WorkflowManager.Data;
using WorkflowManager.Services.Common.Workflow;
using WorkflowManager.ViewModels;

namespace WorkflowManager.Services.Common;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection collection)
    {
        collection.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite("Data Source=WorkflowManager.db");
        }, ServiceLifetime.Singleton);
        
        collection.AddSingleton<IRepository, Repository>();
        collection.AddTransient<IWorkflowService, WorkflowService>();
        collection.AddTransient<MainWindowViewModel>();
        collection.AddTransient<HomeViewModel>();
        

    }
}
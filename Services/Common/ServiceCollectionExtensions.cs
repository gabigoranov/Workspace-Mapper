using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudyPlatform.Data.Common;
using WorkflowManager.Data;
using WorkflowManager.Services.Common.Navigation;
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
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddSingleton<SidebarViewModel>();
        
        collection.AddTransient<HomeViewModel>();
        collection.AddTransient<CreateWorkflowViewModel>();
        
        collection.AddSingleton<IWorkflowService, WorkflowService>();
        collection.AddSingleton<INavigationService, NavigationService>();
        
    }
}
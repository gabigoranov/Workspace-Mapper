using System;
using System.IO;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudyPlatform.Data.Common;
using WorkflowManager.Data;
using WorkflowManager.Services.Common.Navigation;
using WorkflowManager.Services.Common.Workflow;
using WorkflowManager.Services.Dialog;
using WorkflowManager.Services.Process;
using WorkflowManager.Services.WorkflowState;
using WorkflowManager.ViewModels;

namespace WorkflowManager.Services.Common;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection collection)
    {
        collection.AddDbContext<ApplicationDbContext>(options =>
        {
            // This gets the folder where the .exe is running
            var basePath = AppContext.BaseDirectory; 
            // This points to the DB file in that same folder
            var dbPath = Path.Combine(basePath, "WorkflowManager.db");
        
            options.UseSqlite($"Data Source=D:\\Projects\\WorkflowManager\\WorkflowManager.db");
        }, ServiceLifetime.Singleton);
        
        collection.AddSingleton<IRepository, Repository>();
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddSingleton<SidebarViewModel>();
        collection.AddSingleton<IWorkflowStateService, WorkflowStateService>();
        
        collection.AddTransient<HomeViewModel>();
        collection.AddTransient<CreateWorkflowViewModel>();
        collection.AddTransient<UpdateWorkflowViewModel>();
        
        collection.AddScoped<IProcessService, ProcessService>();
        collection.AddScoped<IWorkflowService, WorkflowService>();
        collection.AddScoped<IDialogService>(sp =>
        {
            // 'MainWindow' must exist at this point; otherwise use a factory method
            return new DialogService(() => App.Current!.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
                ? desktop.MainWindow
                : throw new InvalidOperationException("No main window found"));
        });

        
        collection.AddSingleton<INavigationService, NavigationService>();
        
    }
}
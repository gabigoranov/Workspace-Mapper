using System;
using System.IO;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudyPlatform.Data.Common;
using WorkflowManager.Data;
using WorkflowManager.Services.AutoMapper;
using WorkflowManager.Services.Dialog;
using WorkflowManager.Services.Navigation;
using WorkflowManager.Services.Process;
using WorkflowManager.Services.Startup;
using WorkflowManager.Services.Workflow;
using WorkflowManager.Services.WorkflowState;
using WorkflowManager.ViewModels;
using WorkflowManager.ViewModels.Partial;

namespace WorkflowManager.Services.Common;

public static class ServiceCollectionExtensions
{
    
    private static IStartupService GetOsDependentStartupService()
    {
        if (OperatingSystem.IsWindows())
        {
            return new WindowsStartupService();
        }
        else if (OperatingSystem.IsLinux())
        {
            return new LinuxStartupService();
        }
        else if(OperatingSystem.IsMacOS())
        {
            return new MacStartupService();
        }
        
        throw new ArgumentException("Operating System not supported");
    }
    
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
        
        collection.AddSingleton<INavigationService, NavigationService>();
        collection.AddSingleton<IRepository, Repository>();
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddSingleton<SidebarViewModel>();
        collection.AddSingleton<IWorkflowStateService, WorkflowStateService>();
        collection.AddSingleton<IStartupService>(sp =>
            GetOsDependentStartupService());
        collection.AddSingleton<IDialogService>(sp =>
        {
            // 'MainWindow' must exist at this point; otherwise use a factory method
            return new DialogService(() => App.Current!.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
                ? desktop.MainWindow
                : throw new InvalidOperationException("No main window found"));
        });
        
        collection.AddTransient<HomeViewModel>();
        collection.AddTransient<CreateWorkflowViewModel>();
        collection.AddTransient<WorkflowListViewModel>();
        collection.AddTransient<UpdateWorkflowViewModel>();
        
        collection.AddTransient<IProcessService, ProcessService>();
        collection.AddTransient<IWorkflowService, WorkflowService>();
        

        
        
    }
}
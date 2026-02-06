using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using System.Threading.Tasks;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.Services.AutoMapper;
using WorkflowManager.Services.Common;
using WorkflowManager.Services.Common.Navigation;
using WorkflowManager.ViewModels;
using WorkflowManager.Views;

namespace WorkflowManager;

public class App : Application
{
    private static IServiceProvider Services { get; set; } = null!;
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        var services = new ServiceCollection();
        services.AddLogging();
        services.AddAutoMapper(cfg => { }, typeof(MappingProfile));
        services.AddCommonServices();

        Services = services.BuildServiceProvider();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var main = Services.GetRequiredService<MainWindowViewModel>();
            var nav = Services.GetRequiredService<INavigationService>();

            nav.Navigate<HomeViewModel>();

            desktop.MainWindow = new MainWindow
            {
                DataContext = main
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void ExitApplication(object? sender, EventArgs eventArgs)
    {
        Environment.Exit(0);
    }

    private async void ShowApplication(object? sender, EventArgs e)
    {
        while (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop
               || desktop.MainWindow is null)
        {
            await Task.Delay(50); // small delay to avoid blocking
        }
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime loadedDesktop)
        {
            var window = loadedDesktop.MainWindow!;

            window.Show();

            // Schedule activation after layout is complete
            Dispatcher.UIThread.Post(() =>
            {
                window.Activate();
            });

        }
    }

}
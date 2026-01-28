using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.Services.Common;
using WorkflowManager.Services.Common.Navigation;
using WorkflowManager.ViewModels;
using WorkflowManager.Views;

namespace WorkflowManager;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = null!;
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        var services = new ServiceCollection();
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

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
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
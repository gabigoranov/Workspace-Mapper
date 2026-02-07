using System;
using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.ViewModels;

namespace WorkflowManager.Services.Navigation;

public class NavigationService(IServiceProvider services) : INavigationService
{
    public event Action<ViewModelBase>? Navigated;

    public void Navigate<T>() where T : ViewModelBase
    {
        // Load the required services for the view model and DI them before navigating.
        var vm = services.GetRequiredService<T>();
        Navigated?.Invoke(vm);
    }
}

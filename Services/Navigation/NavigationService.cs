using System;
using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.ViewModels;

namespace WorkflowManager.Services.Common.Navigation;

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _services;

    public event Action<ViewModelBase>? Navigated;

    public NavigationService(IServiceProvider services)
    {
        _services = services;
    }

    public void Navigate<T>() where T : ViewModelBase
    {
        var vm = _services.GetRequiredService<T>();
        Navigated?.Invoke(vm);
    }
}

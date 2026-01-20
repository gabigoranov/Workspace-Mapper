using System;
using WorkflowManager.ViewModels;

namespace WorkflowManager.Services.Common.Navigation;

public interface INavigationService
{
    void Navigate<T>() where T : ViewModelBase;
    event Action<ViewModelBase> Navigated;
}

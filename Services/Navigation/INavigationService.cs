using System;
using WorkflowManager.ViewModels;

namespace WorkflowManager.Services.Navigation;

public interface INavigationService
{
    /// <summary>
    /// Navigates to the specified ViewModel's view.
    /// </summary>
    /// <typeparam name="T">The ViewModel to navigate to.</typeparam>
    void Navigate<T>() where T : ViewModelBase;
    event Action<ViewModelBase> Navigated;
}

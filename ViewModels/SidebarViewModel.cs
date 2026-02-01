using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkflowManager.Services.Common.Navigation;
using WorkflowManager.Services.Startup;

namespace WorkflowManager.ViewModels;

public partial class SidebarViewModel(INavigationService navigation, IStartupService startup) : ViewModelBase
{
    [ObservableProperty]
    private bool _isStartupApp = startup.IsEnabled();
    
    partial void OnIsStartupAppChanged(bool value)
    {
        if (value)
            startup.Enable();
        else
            startup.Disable();
    }
    
    [RelayCommand]
    private void GoHome()
        => navigation.Navigate<HomeViewModel>();

    [RelayCommand]
    private void GoCreateWorkflow()
        => navigation.Navigate<CreateWorkflowViewModel>();
}
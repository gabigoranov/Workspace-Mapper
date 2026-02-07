using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkflowManager.Services.Navigation;
using WorkflowManager.Services.Startup;

namespace WorkflowManager.ViewModels;

public partial class SidebarViewModel(INavigationService navigation, IStartupService startup) : ViewModelBase
{
    [ObservableProperty]
    private bool _isStartupApp = startup.IsEnabled();

    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(IsHomeSelected))]
    [NotifyPropertyChangedFor(nameof(IsSettingsSelected))]
    private string _selectedNavItem = "Home";
    
    partial void OnIsStartupAppChanged(bool value)
    {
        if (value)
            startup.Enable();
        else
            startup.Disable();
    }

    [RelayCommand]
    private void GoHome()
    {
        SelectedNavItem = "Home";
        navigation.Navigate<HomeViewModel>();
    }

    [RelayCommand]
    private void GoCreateWorkflow()
        => navigation.Navigate<CreateWorkflowViewModel>();

    [RelayCommand]
    private void GoSettings()
    {
        SelectedNavItem = "Settings";
    }
    
    public bool IsHomeSelected => SelectedNavItem == "Home";
    public bool IsSettingsSelected => SelectedNavItem == "Settings";
}
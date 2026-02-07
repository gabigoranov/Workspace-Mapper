using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkflowManager.Services.Navigation;
using WorkflowManager.Services.Startup;

namespace WorkflowManager.ViewModels;

public partial class SidebarViewModel(INavigationService navigation) : ViewModelBase
{
    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(IsHomeSelected))]
    [NotifyPropertyChangedFor(nameof(IsSettingsSelected))]
    private string _selectedNavItem = "Home";
    
    // Methods to navigate between pages while updating the sidebar selected item
    [RelayCommand]
    private void GoHome()
    {
        SelectedNavItem = "Home";
        navigation.Navigate<HomeViewModel>();
    }

    [RelayCommand]
    private void GoCreateWorkflow()
        => navigation.Navigate<WorkflowEditorViewModel>();

    [RelayCommand]
    private void GoSettings()
    {
        SelectedNavItem = "Settings";
    }
    
    public bool IsHomeSelected => SelectedNavItem == "Home";
    public bool IsSettingsSelected => SelectedNavItem == "Settings";
}
using CommunityToolkit.Mvvm.ComponentModel;
using WorkflowManager.Services.Navigation;

namespace WorkflowManager.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase? _currentViewModel;

    public SidebarViewModel Sidebar { get; }

    public MainWindowViewModel(
        INavigationService navigation,
        SidebarViewModel sidebar)
    {
        Sidebar = sidebar;
        navigation.Navigated += vm => CurrentViewModel = vm;
    }
}
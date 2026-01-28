using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using WorkflowManager.Models;
using WorkflowManager.Services.Common.Navigation;
using WorkflowManager.Services.Common.Workflow;
using WorkflowManager.Services.Startup;

namespace WorkflowManager.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase? _currentViewModel;
    private readonly IStartupService _startup;

    [ObservableProperty]
    private bool _isStartupApp;
    
    public SidebarViewModel Sidebar { get; }
    

    public MainWindowViewModel(
        INavigationService navigation,
        SidebarViewModel sidebar,
        IStartupService  startup) 
    {
        Sidebar = sidebar;
        _startup = startup; 
        _isStartupApp = startup.IsEnabled();

        navigation.Navigated += vm => CurrentViewModel = vm;
    }

    
    partial void OnIsStartupAppChanged(bool value)
    {
        if (value)
            _startup.Enable();
        else
            _startup.Disable();
    }
}
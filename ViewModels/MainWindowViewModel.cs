using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using WorkflowManager.Models;
using WorkflowManager.Services.Common.Navigation;
using WorkflowManager.Services.Common.Workflow;

namespace WorkflowManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase? _currentViewModel;

    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel;
        set => this.RaiseAndSetIfChanged(ref _currentViewModel, value);
    }

    public SidebarViewModel Sidebar { get; }

    public MainWindowViewModel(
        INavigationService navigation,
        SidebarViewModel sidebar)
    {
        Sidebar = sidebar;

        navigation.Navigated += vm => CurrentViewModel = vm;
    }
}
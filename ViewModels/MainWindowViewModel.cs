using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using WorkflowManager.Models;
using WorkflowManager.Services.Navigation;
using WorkflowManager.Services.Startup;

namespace WorkflowManager.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase? _currentViewModel;


    
    public SidebarViewModel Sidebar { get; }
    

    public MainWindowViewModel(
        INavigationService navigation,
        SidebarViewModel sidebar) 
    {
        Sidebar = sidebar;
        navigation.Navigated += vm => CurrentViewModel = vm;
    }

    

}
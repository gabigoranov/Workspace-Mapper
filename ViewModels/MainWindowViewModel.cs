using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using WorkflowManager.Models;
using WorkflowManager.Services.Common.Workflow;

namespace WorkflowManager.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set => this.RaiseAndSetIfChanged(ref _currentViewModel, value);
    }

    private readonly HomeViewModel _homeViewModel;

    public MainWindowViewModel(HomeViewModel homeViewModel)
    {
        _homeViewModel = homeViewModel;

        CurrentViewModel = _homeViewModel; // initial
    }

    public void GoHome() => CurrentViewModel = _homeViewModel;
}

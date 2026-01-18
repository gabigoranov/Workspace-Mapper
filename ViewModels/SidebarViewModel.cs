using CommunityToolkit.Mvvm.Input;
using WorkflowManager.Services.Common.Navigation;

namespace WorkflowManager.ViewModels;

public partial class SidebarViewModel(INavigationService navigation) : ViewModelBase
{
    [RelayCommand]
    private void GoHome()
        => navigation.Navigate<HomeViewModel>();

    [RelayCommand]
    private void GoCreateWorkflow()
        => navigation.Navigate<CreateWorkflowViewModel>();
}
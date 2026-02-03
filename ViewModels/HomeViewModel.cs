using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkflowManager.Models;
using WorkflowManager.Services.Common.Navigation;
using WorkflowManager.Services.Common.Workflow;
using WorkflowManager.Services.Process;
using WorkflowManager.Services.WorkflowState;
using WorkflowManager.ViewModels.Partial;

namespace WorkflowManager.ViewModels;

public partial class HomeViewModel(INavigationService navigation, WorkflowListViewModel workflowList)
    : ViewModelBase
{
    // Expose it to the View
    public WorkflowListViewModel WorkflowList { get; } = workflowList;
    
    [ObservableProperty] private string _searchText = string.Empty;
    
    [RelayCommand]
    private void GoCreateWorkflow()
    {
        navigation.Navigate<CreateWorkflowViewModel>();
    }
}
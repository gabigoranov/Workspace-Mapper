using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkflowManager.Models;
using WorkflowManager.Services.Navigation;
using WorkflowManager.Services.Process;
using WorkflowManager.Services.WorkflowState;
using WorkflowManager.ViewModels.Partial;

namespace WorkflowManager.ViewModels;

public partial class HomeViewModel(INavigationService navigation, WorkflowListViewModel workflowList, IWorkflowStateService workflowStateService)
    : ViewModelBase
{
    // Expose it to the View
    public WorkflowListViewModel WorkflowList { get; } = workflowList;
    
    [ObservableProperty] private string _searchText = string.Empty;
    
    [RelayCommand]
    private void GoCreateWorkflow()
    {
        // clear selected workflow to not enter in edit mode
        workflowStateService.SelectedWorkflow = null;
        navigation.Navigate<WorkflowEditorViewModel>();
    }
}
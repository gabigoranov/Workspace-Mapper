using System.Diagnostics;
using System.Threading.Tasks;
using WorkflowManager.Models;
using WorkflowManager.Services.Common.Workflow;

namespace WorkflowManager.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IWorkflowService  _workflowService;

    public MainWindowViewModel(IWorkflowService workflowService)
    {
        _workflowService = workflowService;
    }

    public async Task CreateWorkflowAsync()
    {
        Debug.WriteLine("testings");
    }
}

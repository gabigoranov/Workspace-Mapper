using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using WorkflowManager.Models;
using WorkflowManager.Services.Common.Navigation;
using WorkflowManager.Services.Common.Workflow;
using Process = WorkflowManager.Models.Process;

namespace WorkflowManager.ViewModels;

public partial class HomeViewModel(IWorkflowService workflowService, INavigationService navigation)
    : ViewModelBase
{
    private readonly IWorkflowService  _workflowService = workflowService;
    
    [ObservableProperty]
    private ObservableCollection<Workflow> _workflows = new(GenerateWorkflows());

    [RelayCommand]
    private void GoCreateWorkflow()
    {
        navigation.Navigate<CreateWorkflowViewModel>();
    }
    
    [RelayCommand]
    private async Task EditWorkflow()
    {
        Debug.WriteLine("testing");
        await Task.CompletedTask;
    }
    
    [RelayCommand]
    private async Task DeleteWorkflow()
    {
        Debug.WriteLine("testing");
        await Task.CompletedTask;
    }
    
    public static List<Workflow> GenerateWorkflows(int workflowCount = 5, int processesPerWorkflow = 3)
    {
        var workflows = new List<Workflow>();

        for (int i = 1; i <= workflowCount; i++)
        {
            var workflow = new Workflow
            {
                Id = i,
                Title = $"Workflow {i}",
                Status = i % 2 == 0 ? WorkflowStatus.Inactive : WorkflowStatus.Active,
                Processes = new List<Process>()
            };

            for (int j = 1; j <= processesPerWorkflow; j++)
            {
                var process = new Process
                {
                    Id = (i - 1) * processesPerWorkflow + j,
                    Title = $"Process {j} of Workflow {i}",
                    Directory = $@"C:\Workflows\Workflow{i}\Process{j}",
                    WorkflowId = workflow.Id,
                    Workflow = workflow,
                    Command = j % 2 == 0 ? $"echo Process {j}" : string.Empty
                };

                workflow.Processes.Add(process);
            }

            workflows.Add(workflow);
        }

        return workflows;
    }
    
    
}
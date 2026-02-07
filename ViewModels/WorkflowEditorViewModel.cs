using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkflowManager.Models;
using WorkflowManager.Models.Common;
using WorkflowManager.Services.Navigation;
using WorkflowManager.Services.Workflow;
using WorkflowManager.Services.WorkflowState;
using WorkflowManager.ViewModels.Binding;

namespace WorkflowManager.ViewModels;

/// <summary>
/// A combined view model for the creation and updating of Workflows, along with their processes
/// </summary>
public partial class WorkflowEditorViewModel : ViewModelBase
{
    public WorkflowEditorViewModel(IWorkflowService workflowService, IWorkflowStateService workflowStateService,
        INavigationService navigation, IMapper mapper,
        ProcessEditorViewModel processEditorViewModel)
    {
        this._workflowService = workflowService;
        this._workflowStateService = workflowStateService;
        this._navigation = navigation;
        this._mapper = mapper;
        this._processEditorViewModel = processEditorViewModel;

        // If the selected workflow is not null => the view was opened for editing
        if (workflowStateService.SelectedWorkflow != null)
        {
            SetupEditMode(workflowStateService.SelectedWorkflow);
        }

        HeaderText = IsEditingMode ? "Edit Workflow" : "Create Workflow";
        SubmitButtonText = IsEditingMode ? "Update Workflow" : "Save workflow";
    }

    /// <summary>
    /// Setup necessary form bindings to edit an existing workflow
    /// </summary>
    /// <param name="model">The non-null model</param>
    private void SetupEditMode(Workflow model)
    {
        IsEditingMode = true;
            
        Title = model.Title;
        ProcessEditorViewModel.AddedProcesses = _mapper.Map<ObservableCollection<ProcessBindingModel>>(model.Processes);
    }

    //DI services
    private readonly IWorkflowService _workflowService;
    private readonly IWorkflowStateService _workflowStateService;
    private readonly INavigationService _navigation;
    private readonly IMapper _mapper;

    // Properties to manage the view based on the current mode - editing / creating
    [ObservableProperty] private bool _isEditingMode;

    [ObservableProperty] private string _headerText;
    [ObservableProperty] private string _submitButtonText;

    /// <summary>
    /// Data bindings for the workflow
    /// </summary>
    [ObservableProperty]
    [Required(ErrorMessage = "A title is required")]
    [MaxLength(30, ErrorMessage = "Title must be less than 30 characters!")]
    private string _title = string.Empty;

    /// <summary>
    /// Data bindings + RelayCommands for the process editor
    /// </summary>
    [ObservableProperty] private ProcessEditorViewModel _processEditorViewModel;

    /// <summary>
    /// Logic for saving a workflow
    /// </summary>
    [RelayCommand]
    private async Task SaveOrEditWorkflow()
    {
        if (IsEditingMode)
        {
            // Update the workflow by taking advantage of the workflowStateService.SelectedWorkflow which was set when first entering in edit mode and should not be null
            var model = _workflowStateService.SelectedWorkflow;
            await EditWorkflow(model);
        }
        else await SaveWorkflow();

        _navigation.Navigate<HomeViewModel>();

        // Reset ProcessEditor
        ProcessEditorViewModel.Reset();
        Title = string.Empty;
    }

    /// <summary>
    /// Edits the workflow using the SelectedWorkflow from state management
    /// </summary>
    /// <exception cref="ArgumentException">Throws if SelectedWorkflow is somehow null while in edit mode</exception>
    private async Task EditWorkflow(Workflow? model)
    {
        if (model == null)
            throw new ArgumentException("The selected workflow was null during editing!");
            
        model.Title = Title;
        model.Processes =
            _mapper.Map<List<Process>>(ProcessEditorViewModel.AddedProcesses);

        await _workflowService.UpdateWorkflowAsync(model.Id, model);

        // Reset the selected workflow to not open the create view in editing mode
        _workflowStateService.SelectedWorkflow = null;
    }

    /// <summary>
    /// Saves workflow to db
    /// </summary>
    private async Task SaveWorkflow()
    {
        // Only validate the Workflow-level fields (like Workflow Title)
        this.ValidateAllProperties();
        if (HasErrors) return;
        
        Workflow model = new Workflow()
        {
            Title = this.Title,
            Processes = _mapper.Map<List<Process>>(ProcessEditorViewModel.AddedProcesses)
        };

        await _workflowService.CreateWorkflowAsync(model);
    }
}
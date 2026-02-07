using System;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using WorkflowManager.Models.Common;
using WorkflowManager.ViewModels.Binding;
using WorkflowManager.ViewModels.Common;

namespace WorkflowManager.ViewModels;

/// <summary>
/// Based on the BindingModel.Discriminator the view will display the correct PartialView for a ProcessBindingModel and we'll cast it later
/// </summary>
public partial class ProcessEditorViewModel : ObservableValidator
{
    private readonly IMapper _mapper;
    
    /// <summary>
    /// Expose all the possible process types to the view
    /// </summary>
    [ObservableProperty]
    private ProcessType[] _processTypes = Enum.GetValues(typeof(ProcessType)).Cast<ProcessType>().ToArray();
    
    /// <summary>
    /// A list of the processes added to the workflow
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<ProcessBindingModel> _addedProcesses = new ObservableCollection<ProcessBindingModel>();
    
    /// <summary>
    /// Contains all the model bindings for the view form + declares the default ProcessType
    /// </summary>
    [ObservableProperty] 
    private ProcessBindingModel _bindingModel = new ProcessBindingModel();

    /// <summary>
    /// State for whether a process is open for editing
    /// </summary>
    [ObservableProperty] private bool _isEditing;

    /// <summary>
    /// The index of the process we are editing in the AddedProcesses list
    /// </summary>
    private int? _editingIndex;

    public ProcessEditorViewModel(IMapper mapper)
    {
        _mapper = mapper;
        
        // Subscribe the view to know when the BindingModel.Discriminator is changed in order to cast it to a new type
        this.WhenAnyValue(x => x.BindingModel.Discriminator)
            .Subscribe(HandleDiscriminatorChange);
    }
    
    /// <summary>
    /// Casts the BindingModel to the appropriate new type
    /// </summary>
    /// <param name="newType">The new ProcessType</param>
    private void HandleDiscriminatorChange(ProcessType newType)
    {
        // Check whether it is already the correct type
        Type targetType = ProcessRegistry.ProcessOptions[newType].GetType();
        if(BindingModel.GetType() == targetType)
            return;
        
        BindingModel = (ProcessBindingModel)_mapper.Map(BindingModel,  BindingModel.GetType(), targetType);
    }

    /// <summary>
    /// Adds a process to the AddedProcesses or Edits an existing process
    /// </summary>
    [RelayCommand]
    private void AddOrEditProcess()
    {
        if (!BindingModel.Validate()) return;
        
        if (IsEditing && _editingIndex.HasValue)
        {
            // Use the indexer [] to replace the item in the collection
            AddedProcesses[_editingIndex.Value] = BindingModel;
    
            IsEditing = false;
            _editingIndex = null; // Clear the index for safety
    
            // Also reset the form after editing is done
            BindingModel = new ProcessBindingModel(); 
        }
        else
        {
            // Clone the model so the list item is a separate instance from the form
            var processToAdd = (ProcessBindingModel)_mapper.Map(BindingModel, BindingModel.GetType(), BindingModel.GetType());
            AddedProcesses.Add(processToAdd);
    
            BindingModel = new ProcessBindingModel(); 
        }
    }

    /// <summary>
    /// Removes a specified process from the AddedProcesses
    /// </summary>
    /// <param name="toDelete">The process model to remove</param>
    [RelayCommand]
    private void DeleteProcess(ProcessBindingModel toDelete)
    {
        AddedProcesses.Remove(toDelete);
    }

    /// <summary>
    /// Opens a process for editing
    /// </summary>
    /// <param name="toEdit">The process to be edited</param>
    [RelayCommand]
    private void StartProcessEdit(ProcessBindingModel toEdit)
    {
        IsEditing = true;
        _editingIndex = AddedProcesses.IndexOf(toEdit);
        
        // Populate the form with the toEdit's data
        // Create a copy to separate it while editing
        var processToEdit = (ProcessBindingModel)_mapper.Map(toEdit, toEdit.GetType(), toEdit.GetType());

        BindingModel = processToEdit;
    }
    
    public void Reset()
    {
        AddedProcesses.Clear();
        BindingModel = new ProcessBindingModel(); 
        IsEditing = false;
    }
}
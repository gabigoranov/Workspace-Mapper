using System.Collections.Generic;
using WorkflowManager.Models.Common;
using WorkflowManager.ViewModels.Binding;

namespace WorkflowManager.ViewModels.Common;

/// <summary>
/// A centralized static declaration of all the possible process form binding models.
/// </summary>
public static class ProcessRegistry
{
    public static readonly Dictionary<ProcessType, ProcessBindingModel> ProcessOptions = new  Dictionary<ProcessType, ProcessBindingModel>()
    {
        { ProcessType.CommandProcess, new CommandProcessBindingModel()}
    };    
}


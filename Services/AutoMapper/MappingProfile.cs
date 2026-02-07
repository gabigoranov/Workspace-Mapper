using AutoMapper;
using WorkflowManager.Models;
using WorkflowManager.Models.Common;
using WorkflowManager.ViewModels.Binding;

namespace WorkflowManager.Services.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // 1. Base Mapping: Tells AutoMapper to look for derived types
        CreateMap<ProcessBindingModel, Models.Common.Process>()
            .Include<CommandProcessBindingModel, CommandProcess>()
            .ReverseMap();

        // 2. Derived Mapping: Handles the specific fields (Command, Directory)
        CreateMap<CommandProcessBindingModel, CommandProcess>()
            .ReverseMap();

        // 3. UI Utility: Used in the "Assemble" step to copy base data to the subclass
        CreateMap<ProcessBindingModel, CommandProcessBindingModel>();
        
        // 4. For cloning
        CreateMap<CommandProcessBindingModel, CommandProcessBindingModel>();
    }
}
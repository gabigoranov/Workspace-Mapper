using AutoMapper;
using WorkflowManager.Models;
using WorkflowManager.ViewModels.Binding;

namespace WorkflowManager.Services.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Models.Common.Process, CommandProcess>()
            .ReverseMap(); 
        
        CreateMap<Models.Common.Process, ProcessBindingModel>()
            .ReverseMap(); 
    }
}
using AutoMapper;
using ESys.API.EndPoints.BusinessForm.GenerateJwtForBusinessForm;
using ESys.Application.CQRS.JWT.Commands.BusinessForm;

namespace ESys.API.Profiles.AutoMappers;

public class GenerateJwtForBusinessFormMapper : Profile
{
    public GenerateJwtForBusinessFormMapper()
    {
        CreateMap<GenerateJwtForBusinessFormRequest, GenerateJwtForBusinessFormCommand>();
    }
}
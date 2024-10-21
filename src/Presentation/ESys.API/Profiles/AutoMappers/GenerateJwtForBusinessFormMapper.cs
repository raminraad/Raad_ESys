using AutoMapper;
using ESys.API.EndPoints.BusinessForm.GenerateJwtForBusinessForm;
using ESys.API.EndPoints.BusinessForm.RedirectToBusinessForm;
using ESys.Application.CQRS.BusinessForm.Commands.GenerateJwtForBusinessForm;
using ESys.Application.CQRS.BusinessForm.Commands.RedirectToBusinessForm;

namespace ESys.API.Profiles.AutoMappers;

public class GenerateJwtForBusinessFormMapper : Profile
{
    public GenerateJwtForBusinessFormMapper()
    {
        CreateMap<GenerateJwtForBusinessFormRequest, GenerateJwtForBusinessFormCommand>().ReverseMap();
        CreateMap<RedirectToBusinessFormRequest, RedirectToBusinessFormCommand>().ReverseMap();
        CreateMap<RedirectToBusinessFormResponse, RedirectToBusinessFormCommandResult>().ReverseMap();
    }
}
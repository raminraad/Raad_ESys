using AutoMapper;
using ESys.API.EndPoints.BusinessForm.GenerateBusinessFormUrl;
using ESys.API.EndPoints.BusinessForm.GenerateJwtForBusinessForm;
using ESys.Application.Features.BusinessForm.Commands.GenerateBusinessFormUrl;
using ESys.Application.Features.BusinessForm.Commands.GenerateJwtForBusinessForm;

namespace ESys.API.Profiles.AutoMappers;

public class GenerateBusinessFormUrlMapper : Profile
{
    public GenerateBusinessFormUrlMapper()
    {
        CreateMap<GenerateJwtForBusinessFormRequest, GenerateJwtForBusinessFormCommand>().ReverseMap();
        CreateMap<GenerateBusinessFormUrlRequest, GenerateBusinessFormUrlCommand>().ReverseMap();
    }
}
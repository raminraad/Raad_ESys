using AutoMapper;
using ESys.Application.CQRS.BusinessForm.Commands.RedirectToBusinessForm;
using ESys.Application.Models;

namespace ESys.Application.Profiles.AutoMappers;

public class BusinessFormDtoMapper : Profile
{
    public BusinessFormDtoMapper()
    {
        CreateMap<RedirectToBusinessFormCommand, RedirectToBusinessFormJwtGenerationDto>().ReverseMap();
        CreateMap<OpenClientSessionDto, RedirectToBusinessFormResponse>().ReverseMap();
    }
}
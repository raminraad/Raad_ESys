using AutoMapper;
using ESys.Application.CQRS.BusinessForm.Commands.RedirectToBusinessForm;
using ESys.Application.Models;
using ESys.Persistence.Dapper.Repositories;

namespace ESys.Application.Profiles.AutoMappers;

public class BusinessFormDtoMapper : Profile
{
    public BusinessFormDtoMapper()
    {
        CreateMap<RedirectToBusinessFormCommand, RedirectToBusinessFormJwtGenerationDto>().ReverseMap();
        CreateMap<NewClientSessionDto, RedirectToBusinessFormCommand>().ReverseMap();
        CreateMap<NewClientSessionDto, ClientSession>().ReverseMap();
        CreateMap<NewClientSessionDto, OpenedClientSessionDto>().ReverseMap();
    }
}
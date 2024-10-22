using AutoMapper;
using ESys.Application.Features.BusinessForm.Commands.GenerateBusinessFormUrl;
using ESys.Application.Models;
using ESys.Domain.Entities;

namespace ESys.Application.Profiles.AutoMappers;

public class BusinessFormDtoMapper : Profile
{
    public BusinessFormDtoMapper()
    {
        CreateMap<GenerateBusinessFormUrlCommand, GenerateBusinessFormUrlJwtGenerationDto>(MemberList.Source);
        CreateMap<NewClientSessionDto, GenerateBusinessFormUrlCommand>().ReverseMap();
        CreateMap<NewClientSessionDto, ClientSession>().ReverseMap();
        CreateMap<NewClientSessionDto, OpenedClientSessionDto>().ReverseMap();
        CreateMap<ClientSession, OpenedClientSessionDto>().ReverseMap();
    }
}
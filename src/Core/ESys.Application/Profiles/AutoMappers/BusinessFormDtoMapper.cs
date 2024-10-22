using AutoMapper;
using ESys.Application.Features.BusinessForm.Commands.GenerateBusinessFormUrl;

namespace ESys.Application.Profiles.AutoMappers;

public class BusinessFormDtoMapper : Profile
{
    public BusinessFormDtoMapper()
    {
        CreateMap<GenerateBusinessFormUrlCommand, RequestClientJwtDto>(MemberList.Source);
    }
}
using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace CompanyEmployees;

public class MappingProfile:Profile
{
    public MappingProfile() 
    {
        CreateMap<Company, CompanyDto>()
            .ForMember<string>
            (
                cmpDto => cmpDto.FullAddress,
                opt => opt.MapFrom<string>(cmp => string.Join(' ', cmp.Address, cmp.Country))
            );
    }
}

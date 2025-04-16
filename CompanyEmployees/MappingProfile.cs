using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace CompanyEmployees;

public class MappingProfile:Profile
{
    public MappingProfile() 
    {
        //CreateMap<Company, CompanyDto>()
        //    .ForMember<string>
        //    (
        //        destination => destination.FullAddress,
        //        source => source.MapFrom<string>(source => string.Join(' ', source.Address, source.Country))
        //    );
        CreateMap<Company, CompanyDto>()
            .ForCtorParam
            (
                "FullAddress",
                source => source.MapFrom<string>(cmp => string.Join(' ', cmp.Address, cmp.Country))
            );
    }
}

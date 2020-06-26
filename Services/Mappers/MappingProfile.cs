using AutoMapper;
using Infrastructure.Data;
using Services.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SiteConfigs, SiteConfig>().ReverseMap();
        CreateMap<Menus, Menu>().ReverseMap();
    }
}
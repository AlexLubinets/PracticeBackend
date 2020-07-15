using AutoMapper;
using ProductApp.BLL.Models;
using ProductApp.DAL.Entities;

namespace ProductApp.BLL.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Operation, OperationDTO>()
                .ForMember(dest=>dest.UserName, opt=> opt.MapFrom(src=>src.AppUser.UserName));
            CreateMap<OperationDTO,Operation>()
                .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}

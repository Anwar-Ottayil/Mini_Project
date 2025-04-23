using AutoMapper;
using MiniProject.Dto;
using MiniProject.Models;

namespace MiniProject.Mapper
{
    public class ProfileMapper:Profile
    {
        public ProfileMapper()
        { 
        CreateMap<User,RegisterDto>().ReverseMap();
        CreateMap<User,LoginDto>().ReverseMap();
        CreateMap<Category,CategoryViewDto>().ReverseMap();
        CreateMap<Product, ProductViewDto>().ReverseMap();
        CreateMap<AddProductDto,Product>().ReverseMap();
        }

    }
}

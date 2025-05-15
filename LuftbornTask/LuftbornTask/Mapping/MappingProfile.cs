using Application.DTOs;
using AutoMapper;
using Core.Entities;

namespace LuftbornTask.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>(); 
        }
    }
}

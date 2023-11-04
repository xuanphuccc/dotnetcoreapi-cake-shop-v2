using AutoMapper;
using dotnetcoreapi.cake.shop.domain;

namespace dotnetcoreapi.cake.shop.application
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryRequestDto, Category>()
                .ForMember(
                    dest => dest.CreateAt,
                    opt => opt.Ignore()
                 );
        }
    }
}

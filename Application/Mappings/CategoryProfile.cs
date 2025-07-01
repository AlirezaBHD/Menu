using Application.Dto.Category;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, RestaurantMenuDto>();
        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<Category, CategoryResponse>();
    }
}
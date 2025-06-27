using Application.Dto.Category;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<Category, CategoryResponse>();
    }
}
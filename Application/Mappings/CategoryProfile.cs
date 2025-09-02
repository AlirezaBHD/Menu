using Application.Dto.Category;
using Application.Exceptions;
using Application.Services;
using AutoMapper;
using Domain.Entities.Categories;

namespace Application.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        var isAvailableExpr = SectionService.IsAvailable(DateTime.Now.TimeOfDay);

        CreateMap<Category, MenuCategoryDto>()
            .ForAllMultiLanguageMembers()
            .ForMember(dest => dest.Sections, opt =>
                opt.MapFrom(src =>
                    src.Sections.AsQueryable().Where(isAvailableExpr).OrderByDescending(s => s.CreatedOn)
                )
            );

        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<Category, CategoryResponse>().ForAllMultiLanguageMembers();
        CreateMap<UpdateCategoryRequest, Category>();
        CreateMap<Category, CategoryListResponse>().ForAllMultiLanguageMembers();
        CreateMap<CategoryTranslationDto, CategoryTranslation>();
        CreateMap<CategoryTranslation, CategoryTranslationDto>();
    }
}
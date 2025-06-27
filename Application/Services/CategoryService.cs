using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Application.Services;

public class CategoryService : Service<Category>, ICategoryService
{
    #region Injection

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        : base(mapper, categoryRepository)
    {
    }

    #endregion
}
using Application.Services.Interfaces;
using AutoMapper;
using Domain.RepositoryInterfaces;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    #region Injection

    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    #endregion
}

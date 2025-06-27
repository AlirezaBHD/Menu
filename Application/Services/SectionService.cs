using Application.Services.Interfaces;
using AutoMapper;
using Domain.RepositoryInterfaces;

namespace Application.Services;

public class SectionService : ISectionService
{
    #region Injection

    private readonly ISectionRepository _sectionRepository;
    private readonly IMapper _mapper;

    public SectionService(ISectionRepository sectionRepository, IMapper mapper)
    {
        _sectionRepository = sectionRepository;
        _mapper = mapper;
    }

    #endregion
}

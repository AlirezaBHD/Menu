using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Application.Services;

public class SectionService : Service<Section>, ISectionService
{
    #region Injection

    public SectionService(ISectionRepository sectionRepository, IMapper mapper) 
        : base(mapper, sectionRepository)
    {
    }

    #endregion
}
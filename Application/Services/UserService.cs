using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class UserService : Service<ApplicationUser>, IUserService
{
    #region Injection

    private readonly IUserRepository _userRepository;

    public UserService(IMapper mapper, ILogger<ApplicationUser> logger, IUserRepository userRepository)
        : base(mapper, userRepository, logger)
    {
        _userRepository = userRepository;
    }

    #endregion
}
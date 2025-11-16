using Muno.Application.Dto.Restaurant;
using Muno.Application.Services;
using Muno.Application.Services.Interfaces;
using AutoMapper;
using Muno.Domain.Entities.Restaurants;
using Muno.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Tests.Application;

public class RestaurantServiceTests
{
    private readonly Mock<IRestaurantRepository> _repoMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly Mock<ILogger<Restaurant>> _loggerMock;
    private readonly RestaurantService _service;

    public RestaurantServiceTests()
    {
        _repoMock = new Mock<IRestaurantRepository>();
        _mapperMock = new Mock<IMapper>();
        _fileServiceMock = new Mock<IFileService>();
        _loggerMock = new Mock<ILogger<Restaurant>>();

        _service = new RestaurantService(
            _repoMock.Object,
            _mapperMock.Object,
            _fileServiceMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task CreateRestaurantAsync_ShouldMapAndSave()
    {
        var request = new CreateRestaurantRequest { Name = "Test Restaurant", Address = "Test address"};

        var entity = new Restaurant
        {
            Id = Guid.NewGuid(),
            Address = request.Address,
            Name = request.Name
        };

        _mapperMock.Setup(m => m.Map<CreateRestaurantRequest, Restaurant>(request))
                   .Returns(entity);

        _repoMock.Setup(r => r.AddAsync(entity)).Returns(Task.CompletedTask);
        _repoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

        await _service.CreateRestaurantAsync(request);

        _repoMock.Verify(r => r.AddAsync(It.Is<Restaurant>(r => r.Name == "Test Restaurant")), Times.Once);
        _repoMock.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateRestaurantAsync_ShouldMapAndSaveWithoutLogo()
    {
        var id = Guid.NewGuid();

        var existing = new Restaurant
        {
            Id = id,
            Name = "Old Name",
            Address  = "old address",
            LogoPath = "old.png"
        };

        var dto = new UpdateRestaurantRequest
        {
            Name = "New Name",
            Address  = "new address",
            LogoFile = null
        };

        var mapped = new Restaurant
        {
            Id = id,
            Name = dto.Name,
            Address  = dto.Address,
            LogoPath = "old.png"
        };

        _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(existing);
        _mapperMock.Setup(m => m.Map(dto, existing)).Returns(mapped);
        _repoMock.Setup(r => r.Update(mapped));
        _repoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

        await _service.UpdateRestaurantAsync(id, dto);

        _repoMock.Verify(r => r.Update(It.Is<Restaurant>(r => r.Name == "New Name")), Times.Once);
        _repoMock.Verify(r => r.SaveAsync(), Times.Once);
        _fileServiceMock.Verify(f => f.SaveFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task UpdateRestaurantAsync_ShouldSaveLogoIfProvided()
    {
        var id = Guid.NewGuid();
        var fakeLogo = new Mock<IFormFile>().Object;

        var dto = new UpdateRestaurantRequest
        {
            Name = "Updated",
            Address  = "new address",
            LogoFile = fakeLogo
        };

        var existing = new Restaurant
        {
            Id = id,
            Name = "Old",
            Address = "old address",
            LogoPath = null
        };

        var mapped = new Restaurant
        {
            Id = id,
            Name = dto.Name,
            Address = dto.Address,
            LogoPath = null
        };

        _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(existing);
        _mapperMock.Setup(m => m.Map(dto, existing)).Returns(mapped);
        _fileServiceMock.Setup(f => f.SaveFileAsync(fakeLogo, "restaurant-logos"))
                        .ReturnsAsync("restaurant-logos/new-logo.png");

        _repoMock.Setup(r => r.Update(It.IsAny<Restaurant>()));
        _repoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

        await _service.UpdateRestaurantAsync(id, dto);

        _fileServiceMock.Verify(f => f.SaveFileAsync(fakeLogo, "restaurant-logos"), Times.Once);
        _repoMock.Verify(r => r.Update(It.Is<Restaurant>(r =>
            r.Name == "Updated" &&
            r.LogoPath == "restaurant-logos/new-logo.png"
        )), Times.Once);
        _repoMock.Verify(r => r.SaveAsync(), Times.Once);
    }
}

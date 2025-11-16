using Muno.Application.Dto.MenuItem;
using Muno.Application.Services;
using Muno.Application.Services.Interfaces;
using AutoMapper;
using Muno.Domain.Interfaces.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Muno.Tests.Application;

public class MenuItemServiceTests
{
    private readonly Mock<IMenuItemRepository> _repoMock;
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<MenuItem>> _loggerMock;
    private readonly MenuItemService _service;

    public MenuItemServiceTests()
    {
        _repoMock = new Mock<IMenuItemRepository>();
        _fileServiceMock = new Mock<IFileService>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<MenuItem>>();

        _service = new MenuItemService(
            _mapperMock.Object,
            _repoMock.Object,
            _fileServiceMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task CreateMenuItemAsync_ShouldCreateAndReturnResponse()
    {
        var sectionId = Guid.NewGuid();
        var request = new CreateMenuItemRequest
        {
            Title = "Pizza",
            ImageFile = new Mock<IFormFile>().Object
        };

        var entity = new MenuItem
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            SectionId = sectionId
        };

        var imagePath = "menu-item/pizza.png";
        var responseDto = new MenuItemResponse { Id = entity.Id, Title = entity.Title };

        _mapperMock.Setup(m => m.Map<CreateMenuItemRequest, MenuItem>(request)).Returns(entity);
        _fileServiceMock.Setup(f => f.SaveFileAsync(request.ImageFile, "MenuItem")).ReturnsAsync(imagePath);
        _mapperMock.Setup(m => m.Map<MenuItem, MenuItemResponse>(entity)).Returns(responseDto);

        _repoMock.Setup(r => r.AddAsync(It.IsAny<MenuItem>())).Returns(Task.CompletedTask);
        _repoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

        var result = await _service.CreateMenuItemAsync(sectionId, request);

        result.Should().NotBeNull();
        result.Title.Should().Be("Pizza");
        _repoMock.Verify(r => r.AddAsync(It.Is<MenuItem>(m =>
            m.SectionId == sectionId && m.ImagePath == imagePath)), Times.Once);
        _repoMock.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateMenuItemAsync_ShouldUpdateWithImage()
    {
        var id = Guid.NewGuid();
        var imageFile = new Mock<IFormFile>().Object;
        var dto = new UpdateMenuItemRequest
        {
            Title = "Updated",
            ImageFile = imageFile
        };

        var existing = new MenuItem
        {
            Id = id,
            Title = "Old",
            ImagePath = "old.png"
        };

        var mapped = new MenuItem
        {
            Id = id,
            Title = "Updated",
            ImagePath = "old.png"
        };

        _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(existing);
        _mapperMock.Setup(m => m.Map(dto, existing)).Returns(mapped);
        _fileServiceMock.Setup(f => f.SaveFileAsync(imageFile, "menu-item"))
            .ReturnsAsync("menu-item/new.png");

        _repoMock.Setup(r => r.Update(It.IsAny<MenuItem>()));
        _repoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

        await _service.UpdateMenuItemAsync(id, dto);

        _repoMock.Verify(r => r.Update(It.Is<MenuItem>(m =>
            m.Title == "Updated" && m.ImagePath == "menu-item/new.png")), Times.Once);
        _repoMock.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteMenuItemAsync_ShouldRemoveItem()
    {
        var id = Guid.NewGuid();
        var menuItem = new MenuItem { Id = id, Title = "Steak" };

        _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(menuItem);
        _repoMock.Setup(r => r.Remove(menuItem));
        _repoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

        await _service.DeleteMenuItemAsync(id);

        _repoMock.Verify(r => r.GetByIdAsync(id), Times.Once);
        _repoMock.Verify(r => r.Remove(menuItem), Times.Once);
        _repoMock.Verify(r => r.SaveAsync(), Times.Once);
    }
}
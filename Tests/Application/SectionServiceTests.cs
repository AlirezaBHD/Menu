using Muno.Application.Dto.Section;
using Muno.Application.Services;
using AutoMapper;
using Muno.Domain.Entities;
using Muno.Domain.Entities.MenuItem;
using Muno.Domain.Interfaces.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using MockQueryable;
using Moq;
using Xunit;

namespace Tests.Application;

public class SectionServiceTests
{
    private readonly Mock<ISectionRepository> _repoMock;
    private readonly Mock<IMenuItemRepository> _menuItemRepoMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<Section>> _loggerMock;
    private readonly SectionService _service;

    public SectionServiceTests()
    {
        _repoMock = new Mock<ISectionRepository>();
        _menuItemRepoMock = new Mock<IMenuItemRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<Section>>();

        _service = new SectionService(
            _repoMock.Object,
            _mapperMock.Object,
            _menuItemRepoMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task CreateSectionAsync_ShouldCreateAndReturnSectionResponse()
    {
        var categoryId = Guid.NewGuid();
        var request = new CreateSectionRequest { Title = "Appetizers" };

        var sectionEntity = new Section
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            CategoryId = categoryId
        };

        var responseDto = new SectionResponse
        {
            Id = sectionEntity.Id,
            Title = sectionEntity.Title
        };

        _mapperMock.Setup(m => m.Map<CreateSectionRequest, Section>(request))
                   .Returns(sectionEntity);

        _mapperMock.Setup(m => m.Map<Section, SectionResponse>(sectionEntity))
                   .Returns(responseDto);

        _repoMock.Setup(r => r.AddAsync(It.IsAny<Section>())).Returns(Task.CompletedTask);
        _repoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

        var result = await _service.CreateSectionAsync(categoryId, request);

        result.Should().NotBeNull();
        result.Id.Should().Be(sectionEntity.Id);
        result.Title.Should().Be("Appetizers");

        _repoMock.Verify(r => r.AddAsync(It.Is<Section>(s => s.CategoryId == categoryId)), Times.Once);
        _repoMock.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteSectionAsync_ShouldRemoveSection_WhenExists()
    {
        var sectionId = Guid.NewGuid();
        var section = new Section { Id = sectionId, Title = "Side Dishes" };

        _repoMock.Setup(r => r.GetByIdAsync(sectionId)).ReturnsAsync(section);
        _repoMock.Setup(r => r.Remove(section));
        _repoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

        await _service.DeleteSectionAsync(sectionId);

        _repoMock.Verify(r => r.GetByIdAsync(sectionId), Times.Once);
        _repoMock.Verify(r => r.Remove(section), Times.Once);
        _repoMock.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateSectionAsync_ShouldUpdateTitleAndMenuItems()
    {
        var sectionId = Guid.NewGuid();
        var oldMenuItemId = Guid.NewGuid();
        var fixedMenuItemId = Guid.NewGuid();
        var newMenuItemId = Guid.NewGuid();

        var oldMenuItems = new List<MenuItem>
        {
            new MenuItem { Id = oldMenuItemId, Title = "Old Item" },
            new MenuItem { Id = fixedMenuItemId, Title = "Fixed Item" }
        };

        var newMenuItems = new List<MenuItem>
        {
            new MenuItem { Id = newMenuItemId, Title = "New Item" }
        };

        var existingSection = new Section
        {
            Id = sectionId,
            Title = "Old Title",
            MenuItems = oldMenuItems
        };

        var updateRequest = new UpdateSectionRequest
        {
            Title = "Updated Title",
            MenuItemIds = [fixedMenuItemId, newMenuItemId]
        };

        var allMenuItems = oldMenuItems.Union(newMenuItems).ToList();

        var queryableSection = new List<Section> { existingSection }.AsQueryable().BuildMock();
        var queryableMenuItems = allMenuItems.AsQueryable().BuildMock();

        _repoMock.Setup(r => r.GetQueryable())
            .Returns(queryableSection);

        _menuItemRepoMock.Setup(r => r.GetQueryable())
            .Returns(queryableMenuItems);

        existingSection.Title = updateRequest.Title;
        _mapperMock.Setup(m => m.Map(updateRequest, existingSection))
            .Returns(existingSection);

        _repoMock.Setup(r => r.Update(It.IsAny<Section>()));
        _repoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

        await _service.UpdateSectionAsync(sectionId, updateRequest);

        var expectedIds = new[] { fixedMenuItemId, newMenuItemId };

        _repoMock.Verify(r => r.Update(It.Is<Section>(s =>
            s.Title == "Updated Title" &&
            s.MenuItems.Count == 2 &&
            expectedIds.All(id => s.MenuItems.Any(m => m.Id == id))
        )), Times.Once);

        _repoMock.Verify(r => r.SaveAsync(), Times.Once);
    }
}


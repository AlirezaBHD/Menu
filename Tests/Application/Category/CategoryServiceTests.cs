using Application.Dto.Category;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using MockQueryable;
using Moq;
using Xunit;

namespace Tests.Application.Category;

public class CategoryServiceTests
{
    private readonly Mock<ICategoryRepository> _repoMock;
    private readonly Mock<ISectionRepository> _sectionRepoMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<Domain.Entities.Category>> _loggerMock;
    private readonly CategoryService _service;

    public CategoryServiceTests()
    {
        _repoMock = new Mock<ICategoryRepository>();
        _sectionRepoMock = new Mock<ISectionRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<Domain.Entities.Category>>();

        _service = new CategoryService(
            _repoMock.Object,
            _mapperMock.Object,
            _sectionRepoMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task CreateCategoryAsync_ShouldCreateAndReturnCategoryResponse()
    {
        var restaurantId = Guid.NewGuid();
        var request = new CreateCategoryRequest { Title = "Main Dishes" };

        var categoryEntity = new Domain.Entities.Category
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            RestaurantId = restaurantId
        };

        var responseDto = new CategoryResponse
        {
            Id = categoryEntity.Id,
            Title = categoryEntity.Title
        };

        _mapperMock.Setup(m => m.Map<CreateCategoryRequest, Domain.Entities.Category>(request))
                   .Returns(categoryEntity);

        _mapperMock.Setup(m => m.Map<Domain.Entities.Category, CategoryResponse>(categoryEntity))
                   .Returns(responseDto);

        _repoMock.Setup(r => r.AddAsync(It.IsAny<Domain.Entities.Category>())).Returns(Task.CompletedTask);
        _repoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

        var result = await _service.CreateCategoryAsync(restaurantId, request);

        result.Should().NotBeNull();
        result.Id.Should().Be(categoryEntity.Id);
        result.Title.Should().Be("Main Dishes");

        _repoMock.Verify(r => r.AddAsync(It.Is<Domain.Entities.Category>(c => c.RestaurantId == restaurantId)), Times.Once);
        _repoMock.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteCategoryAsync_ShouldRemoveCategory_WhenExists()
    {
        var categoryId = Guid.NewGuid();
        var category = new Domain.Entities.Category { Id = categoryId, Title = "Starters" };

        _repoMock.Setup(r => r.GetByIdAsync(categoryId))
            .ReturnsAsync(category);

        _repoMock.Setup(r => r.Remove(category));
        _repoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

        await _service.DeleteCategoryAsync(categoryId);

        _repoMock.Verify(r => r.GetByIdAsync(categoryId), Times.Once);
        _repoMock.Verify(r => r.Remove(category), Times.Once);
        _repoMock.Verify(r => r.SaveAsync(), Times.Once);
    }
    [Fact]
    public async Task UpdateCategoryAsync_ShouldUpdateCategoryAndSections()
    {
        var categoryId = Guid.NewGuid();
        var oldSectionId = Guid.NewGuid();
        var fixedSectionId = Guid.NewGuid();
        var newSectionId = Guid.NewGuid();

        var oldSections = new List<Section>
        {
            new Section { Id = oldSectionId, Title = "Old Section" },
            new Section { Id = fixedSectionId, Title = "Fixed Section"}
        };
        
        var newSections = new List<Section>
        {
            new Section { Id = newSectionId, Title = "New Section" }
        };
        
        var existingCategory = new Domain.Entities.Category
        {
            Id = categoryId,
            Title = "Old Name",
            Sections = oldSections
        };

        var updateRequest = new UpdateCategoryRequest
        {
            Title = "New Name",
            SectionIds = [fixedSectionId, newSectionId]
        };


        var allSections = oldSections.Union(newSections).ToList();
        
        var queryableCategory = new List<Domain.Entities.Category> { existingCategory }.AsQueryable().BuildMock();
        var queryableSections = allSections.AsQueryable().BuildMock();

        _repoMock.Setup(r => r.GetQueryable())
            .Returns(queryableCategory);

        _sectionRepoMock.Setup(r => r.GetQueryable())
            .Returns(queryableSections);

        existingCategory.Title = updateRequest.Title;
        _mapperMock.Setup(m => m.Map(updateRequest, existingCategory))
            .Returns(existingCategory);

        _repoMock.Setup(r => r.Update(It.IsAny<Domain.Entities.Category>()));
        _repoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

        await _service.UpdateCategoryAsync(categoryId, updateRequest);

        var expectedIds = new[] { fixedSectionId, newSectionId };
        
        _repoMock.Verify(r => r.Update(It.Is<Domain.Entities.Category>(c =>
            c.Title == "New Name" &&
            c.Sections.Count == 2 &&
            expectedIds.All(id => c.Sections.Any(s => s.Id == id))
        )), Times.Once);
    }
}


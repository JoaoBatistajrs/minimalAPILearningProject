using AutoMapper;
using MinimalAPI.Domain.Entities;
using MinimalAPI.Domain.Interfaces.Repository;
using MinimalAPI.Domain.Models;
using MinimalAPI.Services;
using Moq;
using System.Linq.Expressions;

namespace MinimalAPITests.Services;


[TestClass]
public class CarsServiceTests
{
    private Mock<IEntityRepository<Car>> _entityRepoMock = null!;
    private Mock<ICarsRepository> _carsRepoMock = null!;
    private Mock<IMapper> _mapperMock = null!;
    private CarsService _service = null!;

    [TestInitialize]
    public void Setup()
    {
        _entityRepoMock = new Mock<IEntityRepository<Car>>();
        _carsRepoMock = new Mock<ICarsRepository>();
        _mapperMock = new Mock<IMapper>();

        _service = new CarsService(_entityRepoMock.Object, _mapperMock.Object, _carsRepoMock.Object);
    }

    [TestMethod]
    public async Task Create_ShouldReturnCar_WhenModelIsValid()
    {
        var carModel = new CarModel { Make = "Toyota", Model = "Corolla" };
        var carEntity = new Car { Id = 1, Make = "Toyota", Model = "Corolla" };

        _mapperMock.Setup(m => m.Map<Car>(carModel)).Returns(carEntity);
        _entityRepoMock.Setup(r => r.CreateAsync(carEntity)).ReturnsAsync(carEntity);

        var result = await _service.Create(carModel);

        Assert.IsNotNull(result);
        Assert.AreEqual("Toyota", result!.Make);
        _entityRepoMock.Verify(r => r.CreateAsync(It.IsAny<Car>()), Times.Once);
    }

    [TestMethod]
    public async Task GetCarsAsync_ShouldReturnPagedResult_WhenFilterApplied()
    {
        var pagedResult = new PagedResult<Car>
        {
            Items = new List<Car> { new Car { Make = "Ford", Model = "Fiesta" } },
            TotalCount = 1
        };

        _entityRepoMock.Setup(r => r.GetPagedAsync(It.IsAny<Expression<Func<Car, bool>>>(), 1, 10))
                       .ReturnsAsync(pagedResult);

        var result = await _service.GetCarsAsync("Ford", null, 1, 10);

        Assert.AreEqual(1, result.TotalCount);
        Assert.AreEqual("Ford", result.Items.First().Make);
    }

    [TestMethod]
    public async Task GetById_ShouldReturnCar_WhenIdExists()
    {
        var car = new Car { Id = 1, Make = "Honda", Model = "Civic" };

        _entityRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(car);

        var result = await _service.GetById(1);

        Assert.IsNotNull(result);
        Assert.AreEqual("Honda", result!.Make);
    }

    [TestMethod]
    public async Task GetById_ShouldReturnNull_WhenCarDoesNotExist()
    {
        _entityRepoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Car?)null);

        var result = await _service.GetById(99);

        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task Update_ShouldReturnUpdatedCar_WhenExists()
    {
        var updatedCar = new Car { Id = 1, Make = "Fiat", Model = "Argo" };
        var model = new CarModel { Make = "Fiat", Model = "Argo" };

        _carsRepoMock.Setup(r => r.UpdateAsync(1, model)).ReturnsAsync(updatedCar);

        var result = await _service.Update(1, model);

        Assert.IsNotNull(result);
        Assert.AreEqual("Argo", result!.Model);
    }
}
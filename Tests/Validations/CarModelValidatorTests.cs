using FluentValidation.TestHelper;
using MinimalAPI.Domain.Models;
using MinimalAPI.Helper;


namespace MinimalAPITests.Validations;

[TestClass]
public class CarModelValidatorTests
{
    private CarModelValidator _validator = null!;

    [TestInitialize]
    public void Setup()
    {
        _validator = new CarModelValidator();
    }

    [TestMethod]
    public void Should_Have_Error_When_Make_Is_Empty()
    {
        var model = new CarModel { Make = "", Model = "Corolla", Year = 2020 };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Make)
              .WithErrorMessage("Make is required");
    }

    [TestMethod]
    public void Should_Have_Error_When_Model_Is_Empty()
    {
        var model = new CarModel { Make = "Toyota", Model = "", Year = 2020 };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Model)
              .WithErrorMessage("Model is required");
    }

    [TestMethod]
    public void Should_Have_Error_When_Year_Is_Too_Early()
    {
        var model = new CarModel { Make = "Ford", Model = "Fusion", Year = 1800 };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Year)
              .WithErrorMessage("Year must be a valid year");
    }

    [TestMethod]
    public void Should_Have_Error_When_Year_Is_In_Future()
    {
        var futureYear = DateTime.Now.Year + 1;
        var model = new CarModel { Make = "Tesla", Model = "Model X", Year = futureYear };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Year)
              .WithErrorMessage("Year must be a valid year");
    }

    [TestMethod]
    public void Should_Not_Have_Error_When_Model_Is_Valid()
    {
        var model = new CarModel { Make = "Honda", Model = "Civic", Year = 2022 };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}

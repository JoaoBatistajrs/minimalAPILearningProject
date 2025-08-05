using FluentValidation;
using MinimalAPI.Domain.Models;

namespace MinimalAPI.Helper;

public class CarModelValidator : AbstractValidator<CarModel>
{
    public CarModelValidator()
    {
        RuleFor(c => c.Make).NotEmpty().WithMessage("Make is required");
        RuleFor(c => c.Model).NotEmpty().WithMessage("Model is required");
        RuleFor(c => c.Year).InclusiveBetween(1900, DateTime.Now.Year)
            .WithMessage("Year must be a valid year");
    }
}

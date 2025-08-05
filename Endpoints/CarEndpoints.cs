using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Domain.Interfaces.Service;
using MinimalAPI.Domain.Models;

namespace MinimalAPI.Endpoints;

public static class CarEndpoints
{
    public static void MapCarEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/cars", async (
            [FromServices] ICarsService service,
            [FromQuery] string? make,
            [FromQuery] string? model,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10) =>
        {
            var result = await service.GetCarsAsync(make, model, page, pageSize);
            return Results.Ok(result);
        }).WithTags("Cars").RequireAuthorization();

        app.MapGet("/api/cars/{id}", async (
            [FromRoute] int id,
            [FromServices] ICarsService service,
            [FromServices] ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("CarsEndpoint");

            var car = await service.GetById(id);

            if (car == null)
            {
                logger.LogWarning("Car with id {Id} not found", id);
                return Results.NotFound(new { message = $"Car with id {id} not found." });
            }

            return Results.Ok(car);
        }).WithTags("Cars").RequireAuthorization();

        app.MapPost("/api/cars", async (
            [FromServices] ICarsService service,
            [FromServices] IValidator<CarModel> validator,
            [FromServices] ILoggerFactory loggerFactory,
            [FromBody] CarModel carModel) =>
        {
            var logger = loggerFactory.CreateLogger("CarsEndpoint");

            var validationResult = await validator.ValidateAsync(carModel);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(new
                {
                    errors = validationResult.Errors.Select(e => e.ErrorMessage)
                });
            }

            try
            {
                var createdCar = await service.Create(carModel);

                if (createdCar == null)
                {
                    logger.LogWarning("Car creation returned null");
                    return Results.Problem("Failed to create car", statusCode: 400);
                }

                return Results.Created($"/api/cars/{createdCar.Id}", createdCar);
            }
            catch (DbUpdateException dbEx)
            {
                logger.LogError(dbEx, "Database error while creating car");
                return Results.Problem("A database error occurred.", statusCode: 500);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while creating car");
                return Results.Problem("Unexpected error occurred.", statusCode: 500);
            }
        }).WithTags("Cars").RequireAuthorization();

        app.MapPut("/api/cars/{id}", async (
            [FromRoute] int id,
            [FromBody] CarModel updatedModel,
            [FromServices] ICarsService service,
            [FromServices] IValidator<CarModel> validator,
            [FromServices] ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("CarsEndpoint");
            var validationResult = await validator.ValidateAsync(updatedModel);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(new
                {
                    errors = validationResult.Errors.Select(e => e.ErrorMessage)
                });
            }
            var updatedCar = await service.Update(id, updatedModel);
            if (updatedCar == null)
            {
                logger.LogWarning("Car with id {Id} not found for update", id);
                return Results.NotFound(new { message = $"Car with id {id} not found." });
            }
            return Results.Ok(updatedCar);
        }).WithTags("Cars").RequireAuthorization();
    }
}

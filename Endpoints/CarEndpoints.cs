using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Domain.Interfaces.Service;

namespace MinimalAPI.Endpoints
{
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
            });
        }
    }
}

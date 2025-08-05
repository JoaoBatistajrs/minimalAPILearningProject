using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Domain.Interfaces.Service;
using MinimalAPI.Domain.Models;
using MinimalAPI.Services;

namespace MinimalAPI.Endpoints
{
    public static class UserEndpoint
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/users", () =>
            {
                return Results.Ok();
            }).RequireAuthorization();

            app.MapPost("/user", async ([FromServices] IUserService userService, UserModel userModel) =>
            {
                var user = await userService.CreateUser(userModel);
                return Results.Created($"/user/{user.Id}", user);
            });

        }
    }
}

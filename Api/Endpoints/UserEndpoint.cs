using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Domain.Interfaces.Service;
using MinimalAPI.Domain.Models;
using System.Security.Claims;

namespace MinimalAPI.Endpoints
{
    public static class UserEndpoint
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var userGroup = app.MapGroup("/user")
                               .WithTags("Users")
                               .RequireAuthorization();

            userGroup.MapPost("/create", [Authorize(Roles = "Adm,User")] async (
                [FromServices] IUserService userService,
                UserModel userModel) =>
            {
                var user = await userService.CreateUser(userModel);
                return Results.Created($"/user/{user.Id}", user);
            });

            userGroup.MapPost("/admin/create", [Authorize(Roles = "Adm")] async (
                [FromServices] IUserService userService,
                [FromBody] AdminCreateUserModel userModel,
                ClaimsPrincipal userClaims) =>
            {
                if (!userClaims.IsInRole("Adm"))
                    return Results.Forbid();

                var user = await userService.CreateAdminUser(userModel);
                return Results.Created($"/user/{user.Id}", user);
            });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Domain.Interfaces.Service;
using MinimalAPI.Infrasctructure.DataBase;
using MinimalAPI.Services;

namespace MinimalAPI.Endpoints;

public static class Login
{
    public static void MapLoginEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (UserLoginDto login, CarsContext db,
            [FromServices] IPasswordService passwordService, [FromServices]IJwtService jwtService) =>
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == login.Email);
            if (user is null || !passwordService.VerifyPassword(user, login.Password))
                return Results.Unauthorized();
            var token = jwtService.GenerateToken(user);
            return Results.Ok(new { token });
        });
    }
}

public class UserLoginDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}


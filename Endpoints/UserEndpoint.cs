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

            app.MapPost("/user", () =>
            {
                return Results.Created();
            }).RequireAuthorization();
        }
    }
}

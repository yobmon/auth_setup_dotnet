using eqnet.Models;

using Microsoft.EntityFrameworkCore;
public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/users", async (ModelContext db) =>
        {
            return await db.users.ToListAsync();
        })
        .RequireAuthorization();


        app.MapPost("/auth/register", async (RegisterRequest req, AuthService auth) =>
{
    var token = await auth.Register(req);
    return Results.Ok(new AuthResponse { Token = token });
});

app.MapPost("/auth/login", async (LoginRequest req, AuthService auth) =>
{
    var token = await auth.Login(req);
    return Results.Ok(new AuthResponse { Token = token });
}).RequireRateLimiting("fixed");

    }

}
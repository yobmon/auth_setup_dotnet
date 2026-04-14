using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using eqnet.Models;
public class AuthService
{
    private readonly ModelContext _db;
    private readonly JwtTokenGenerator _jwt;
    private readonly PasswordHasher<User> _hasher = new();

    public AuthService(ModelContext db, JwtTokenGenerator jwt)
    {
        _db = db;
        _jwt = jwt;
    }

    public async Task<string> Register(RegisterRequest request)
    {
        //need to check the email syntax 
        var exists = await _db.users
            .FirstOrDefaultAsync(x => x.Email == request.Email);

        if (exists != null)
            throw new Exception("User already exists");

        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
        };

        user.PasswordHash = _hasher.HashPassword(user, request.Password);

        _db.users.Add(user);
        await _db.SaveChangesAsync();

        return _jwt.GenerateToken(user);
    }

    public async Task<string> Login(LoginRequest request)
    {
        var user = await _db.users
            .FirstOrDefaultAsync(x => x.Email == request.Email);

        if (user == null)
            throw new Exception("user not found");

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new Exception("Invalid credentials");

        return _jwt.GenerateToken(user);
    }
}
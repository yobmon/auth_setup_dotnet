using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



public class User
{
    [Key]
    [Column("user_id")]
    public Guid UserId { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public   string PasswordHash { get; set; }
}
public class RegisterRequest
{
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Password { get; set; }
}
public class LoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
public class AuthResponse
{
    public required string Token { get; set; }
}
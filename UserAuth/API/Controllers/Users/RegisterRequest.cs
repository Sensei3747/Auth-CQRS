namespace UserAuth.API.Controllers.Users;

public record RegisterRequest(string Username, string Email, string Password);
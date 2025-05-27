using UserAuth.Domain.Abstractions;

namespace UserAuth.Application.Abstractions.Auth;

public interface IJwtService
{
    Result<string> GetAccessToken(string email);
}
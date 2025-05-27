using Microsoft.AspNetCore.Authentication.BearerToken;
using UserAuth.Application.Abstractions.Messaging;

namespace UserAuth.Application.Users.Login;

public record LoginUserQuery(string email, string password) : IQuery<AccessTokenResponse>;


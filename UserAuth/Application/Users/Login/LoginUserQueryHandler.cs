using UserAuth.Domain.Entry;
using UserAuth.Application.Abstractions.Auth;
using UserAuth.Application.Abstractions.Messaging;
using UserAuth.Domain.Abstractions;
using Microsoft.AspNetCore.Authentication.BearerToken;
using UserAuth.Domain.Users;
using Azure.Core;

namespace UserAuth.Application.Users.Login;

internal sealed class LoginUserQueryHandler : IQueryHandler<LoginUserQuery, AccessTokenResponse>
{
    private readonly IEntryRepository _entryRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;
    public LoginUserQueryHandler(IEntryRepository entryRepository, IPasswordHasher passwordHasher, IJwtService jwtService)
    {
        _entryRepository = entryRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<Result<AccessTokenResponse>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var entry = await _entryRepository.GetByEmail(request.email);

        if (entry is null)
        {
            return Result.Failure<AccessTokenResponse>(EntryErrors.NotFound);
        }
        var isPasswordValid = _passwordHasher.Verify(request.password, entry.PasswordHash);

        if (!isPasswordValid)
        {
            return Result.Failure<AccessTokenResponse>(EntryErrors.InvalidCredentials);
        }
        var result = _jwtService.GetAccessToken(
            request.email
        );
        if (result.IsFailure)
        {
            return Result.Failure<AccessTokenResponse>(EntryErrors.InvalidCredentials);
        }

        return new AccessTokenResponse(result.Value, entry.Email);
    }   
}
using UserAuth.Application.Abstractions.Messaging;
using UserAuth.Domain.Abstractions;

namespace UserAuth.Application.Users.Register;

public record RegisterUserCommand(string email, string password) : ICommand<string>;
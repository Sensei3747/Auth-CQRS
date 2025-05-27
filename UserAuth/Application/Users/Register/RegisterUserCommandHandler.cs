using UserAuth.Application.Abstractions.Messaging;
using UserAuth.Application.Abstractions.Auth;
using UserAuth.Domain.Abstractions;
using UserAuth.Domain.Entry;
using UserAuth.Application.Users.Register;
using UserAuth.Domain.Users;

namespace UserAuth.Application.Users.Register;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, string>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEntryRepository _entryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IPasswordHasher passwordHasher, IEntryRepository entryRepository, IUnitOfWork unitOfWork)
    {
        _passwordHasher = passwordHasher;
        _entryRepository = entryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUserEmail = await _entryRepository.GetByEmail(request.email);
        if (existingUserEmail is not null)
        {
            return Result.Failure<string>(EntryErrors.AlreadyExists);
        }

        var hashedPassword = _passwordHasher.HashPassword(request.password);
        var entry = Entry.Create(request.email, hashedPassword);

        _entryRepository.Add(entry);
        await _unitOfWork.SaveChangesAsync();

        return entry.Email;

    }
}
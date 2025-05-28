using UserAuth.Application.Abstractions.Auth;
using UserAuth.Application.Abstractions.Clock;
using UserAuth.Application.Abstractions.Messaging;
using UserAuth.Domain.Abstractions;
using UserAuth.Domain.Conversations;
using UserAuth.Domain.Rooms;
using UserAuth.Infrastructure.Repositories;

namespace UserAuth.Application.Rooms.CreateRoom;

internal sealed class CreateRoomCommandHandler : ICommandHandler<CreateRoomCommand, long>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IPasswordHasher _passwordHasher;

    public CreateRoomCommandHandler(IRoomRepository roomRepository, IDateTimeProvider dateTimeProvider, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _roomRepository = roomRepository;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<long>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = _passwordHasher.HashPassword(request.password);

        var room = Room.Create(request.name, hashedPassword, _dateTimeProvider.UtcNow);

        room.AddMember(request.userId, room.Id, Role.Admin, _dateTimeProvider.UtcNow);

        _roomRepository.Add(room);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return room.Id;
    }
}
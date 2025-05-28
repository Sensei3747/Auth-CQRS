using UserAuth.Application.Abstractions.Auth;
using UserAuth.Application.Abstractions.Clock;
using UserAuth.Application.Abstractions.Messaging;
using UserAuth.Domain.Abstractions;
using UserAuth.Domain.Rooms;

namespace UserAuth.Application.Rooms.JoinRoom;

internal sealed class JoinRoomCommandHandler : ICommandHandler<JoinRoomCommand, long>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JoinRoomCommandHandler(IRoomRepository roomRepository, IDateTimeProvider dateTimeProvider, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<long>> Handle(JoinRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdWithMembers(request.roomId);

        var memberExists = room.Members.Where(member => member.UserId == request.userId).Any();

        if (memberExists)
        {
            return Result.Failure<long>(RoomErrors.MemberAlreadyExists);
        }

        room.AddMember(request.userId, room.Id, Role.Member, _dateTimeProvider.UtcNow);

        _roomRepository.Update(room);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return room.Id;
    }
}
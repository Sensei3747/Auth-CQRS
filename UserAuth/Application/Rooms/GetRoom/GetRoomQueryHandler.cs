using UserAuth.Application.Abstractions.Messaging;
using UserAuth.Domain.Abstractions;
using UserAuth.Domain.Rooms;

namespace UserAuth.Application.Rooms.GetRoom;

internal sealed class GetRoomQueryHandler : IQueryHandler<GetRoomQuery, RoomResponse>
{
    private readonly IRoomRepository _roomRepository;
    public GetRoomQueryHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<Result<RoomResponse>> Handle(GetRoomQuery request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdWithMembers(request.roomId);

        if (room is null)
        {
            return Result.Failure<RoomResponse>(Error.NullValue);
        }

        var roomResponse = new RoomResponse()
        {
            Name = room.Name,
            Members = room.Members.Select(member => new MemberResponse
            {
                Name = member.User.Name,
                Role = member.Role.ToString(),
                JoinedOn = member.JoinedOn
            }).ToList()
        };

        return roomResponse;
    }
}
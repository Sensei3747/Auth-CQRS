using UserAuth.Application.Abstractions.Messaging;

namespace UserAuth.Application.Rooms.GetRoom;

public record GetRoomQuery(long roomId) : IQuery<RoomResponse>;
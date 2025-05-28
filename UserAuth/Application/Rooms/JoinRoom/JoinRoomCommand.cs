using UserAuth.Application.Abstractions.Messaging;

namespace UserAuth.Application.Rooms.JoinRoom;

public record JoinRoomCommand(long roomId, long userId) : ICommand<long>;
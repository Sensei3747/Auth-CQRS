using UserAuth.Application.Abstractions.Messaging;

namespace UserAuth.Application.Rooms.CreateRoom;

public record CreateRoomCommand(string name, string password, long userId) : ICommand<long>;
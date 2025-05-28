using UserAuth.Application.Abstractions.Messaging;

namespace UserAuth.Application.Conversations.AddMessage;

public record AddMessageCommand(long roomId, long senderId, string content ) : ICommand<string>;

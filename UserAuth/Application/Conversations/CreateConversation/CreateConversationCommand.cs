using UserAuth.Application.Abstractions.Messaging;
using UserAuth.Domain.Conversations;

namespace UserAuth.Application.Conversations.CreateConversation;

public record CreateConversationCommand(long? roomId, List<ConversationParticipant> participants) : ICommand<long>;
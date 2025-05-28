using UserAuth.Application.Abstractions.Messaging;

namespace UserAuth.Application.Conversations.GetMessagesByConversationId;

public record GetMessagesByConversationIdQuery(long conversationId) : IQuery<List<MessageResponse>>;
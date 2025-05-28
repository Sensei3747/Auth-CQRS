using UserAuth.Application.Abstractions.Messaging;

namespace UserAuth.Application.Conversations.GetConversationByRoomId;

public record GetConversationByRoomIdQuery(long roomId, int page, int pageSize) : IQuery<ConversationResponse>;
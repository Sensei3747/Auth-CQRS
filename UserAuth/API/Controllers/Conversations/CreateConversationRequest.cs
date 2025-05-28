using UserAuth.Domain.Conversations;

namespace UserAuth.API.Controllers.Conversations;
public record CreateConversationRequest(long? roomId, List<ConversationParticipant> participants);
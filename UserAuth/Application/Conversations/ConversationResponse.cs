using UserAuth.Domain.Conversations;

namespace UserAuth.Application.Conversations;

public class ConversationResponse
{
    public long? RoomId { get; init; }
    public List<ConversationParticipant> Participants { get; init; }
    public List<MessageResponse> Messages { get; init; }
}

public class MessageResponse
{
    public string SenderName { get; init; }
    public string Content { get; init; }
    public DateTime CreatedOn { get; init; }
}
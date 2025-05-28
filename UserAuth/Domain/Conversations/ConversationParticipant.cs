namespace UserAuth.Domain.Conversations;
public class ConversationParticipant
{
    public long UserId { get; set; }
    public long ConversationId { get; set; }
    public Conversation Conversation { get; set; } = null!;
}
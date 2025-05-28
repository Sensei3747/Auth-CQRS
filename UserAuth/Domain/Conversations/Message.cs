
namespace UserAuth.Domain.Conversations;

public class Message
{
    private Message(string conversationId, long senderId, string senderName, string content, DateTime createdOn)
    {
        ConversationId = conversationId;
        SenderId = senderId;
        SenderName = senderName;
        Content = content;
        CreatedOn = createdOn;
    }

    public string Id { get; private set; }
    public string ConversationId { get; private set; }
    public long SenderId { get; private set; }
    public string SenderName { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedOn { get; private set; }

    public static Message Create(string conversationId, long senderId, string senderName, string content, DateTime createdOn)
    {
        var message = new Message(conversationId, senderId, senderName, content, createdOn);

        return message;
    }
}
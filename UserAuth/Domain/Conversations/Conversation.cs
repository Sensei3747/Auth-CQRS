

namespace UserAuth.Domain.Conversations;

public class Conversation
{
    private Conversation(long? roomId, List<ConversationParticipant> participants, bool isGroup, DateTime createdOn)
    {
        RoomId = roomId;
        Participants = participants;
        IsGroup = isGroup;
        CreatedOn = createdOn;
    }


    public long Id { get; private set; }
    public long? RoomId { get; private set; } // null for private conversation
    public List<ConversationParticipant> Participants { get; private set; } = new(); // empty for room conversation. userid for user to user chat
    public bool IsGroup { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime UpdatedOn { get; private set; }

    protected Conversation() { } 

    public static Conversation Create(long? roomId, List<ConversationParticipant> participants, bool isGroup, DateTime createdOn)
    {
        if (isGroup)
        {
            if (roomId == null || participants.Any())
                throw new ArgumentException(ConversationErrors.InvalidGroupConversation.message);
        }
        else
        {
            if (roomId != null || participants.Count != 2)
                throw new ArgumentException(ConversationErrors.InvalidDirectConversation.message);
        }

        var conversation = new Conversation(roomId, participants, isGroup, createdOn);

        return conversation;
    }
}
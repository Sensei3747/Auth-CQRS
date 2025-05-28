namespace UserAuth.Domain.Conversations;

public interface IConversationRepository
{
    Task AddAsync(Conversation conversation);
    Task UpdateAsync(long id, Conversation conversation);
    Task RemoveAsync(long id);
    Task<Conversation?> GetByRoomId(long roomId);
    Task<Conversation?> GetByParticipantIds(List<ConversationParticipant> participantIds);
}

namespace UserAuth.Domain.Conversations;

public interface IMessageRepository
{
    Task<List<Message>> GetByConversationIdAsync(long conversationId, int skip, int limit);
    Task AddAsync(Message message);
    Task UpdateAsync(string id, Message message);
    Task RemoveAsync(string id);
}
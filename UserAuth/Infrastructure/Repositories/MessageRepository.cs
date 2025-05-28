using UserAuth.Domain.Conversations;
using UserAuth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace UserAuth.Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly ApplicationDbContext _context;

    public MessageRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<Message>> GetByConversationIdAsync(long conversationId, int skip, int limit)
    {
        return await _context.Set<Message>()
            .Where(m => m.ConversationId == conversationId)
            .OrderBy(m => m.CreatedOn)
            .Skip(skip)
            .Take(limit)
            .ToListAsync();
    }
    public async Task AddAsync(Message message)
    {
        await _context.Set<Message>().AddAsync(message);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(string id, Message message)
    {
        var existing = await _context.Set<Message>().FindAsync(id);
        if (existing != null)
        {
            _context.Entry(existing).CurrentValues.SetValues(message);
            await _context.SaveChangesAsync();
        }
    }
    public async Task RemoveAsync(string id)
    {
        var message = await _context.Set<Message>().FindAsync(id);
        if (message != null)
        {
            _context.Set<Message>().Remove(message);
            await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using UserAuth.Domain.Conversations;
using UserAuth.Infrastructure.Data;

namespace UserAuth.Infrastructure.Repositories;

public class ConversationRepository : IConversationRepository
{
    private readonly ApplicationDbContext _context;

    public ConversationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Conversation conversation)
    {
        await _context.Set<Conversation>().AddAsync(conversation);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(long id, Conversation updatedConversation)
    {
        var conversation = await _context.Set<Conversation>().FindAsync(id);
        if (conversation != null)
        {
            _context.Entry(conversation).CurrentValues.SetValues(updatedConversation);

            // Optionally update participants
            conversation.Participants.Clear();
            foreach (var p in updatedConversation.Participants)
            {
                conversation.Participants.Add(p);
            }

            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveAsync(long id)
    {
        var conversation = await _context.Set<Conversation>().FindAsync(id);
        if (conversation != null)
        {
            _context.Set<Conversation>().Remove(conversation);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Conversation?> GetByRoomId(long roomId)
    {
        return await _context.Set<Conversation>()
            .Include(c => c.Participants)
            .FirstOrDefaultAsync(c => c.RoomId == roomId);
    }

    public async Task<Conversation?> GetByParticipantIds(List<ConversationParticipant> inputParticipants)
    {
        var inputUserIds = inputParticipants.Select(p => p.UserId).OrderBy(x => x).ToList();

        var candidates = await _context.Set<Conversation>()
            .Include(c => c.Participants)
            .Where(c => c.Participants.Count == inputUserIds.Count)
            .ToListAsync();

        return candidates.FirstOrDefault(c =>
            c.Participants.Select(p => p.UserId).OrderBy(x => x).SequenceEqual(inputUserIds));
    }

}

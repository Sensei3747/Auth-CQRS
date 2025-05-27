using UserAuth.Domain.Entry;
using UserAuth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace UserAuth.Infrastructure.Repositories;

internal sealed class EntryRepository : IEntryRepository
{
    private readonly ApplicationDbContext _context;

    public EntryRepository(ApplicationDbContext dbContext)
    {
        _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public void Add(Entry entry)
    {
        if (entry == null)
        {
            throw new ArgumentNullException(nameof(entry), "Entry cannot be null.");
        }
        _context.Set<Entry>().Add(entry);
    }

    public async Task<Entry?> GetByEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));
        }

        var entry = await _context.Set<Entry>().Where(entry => entry.Email == email).FirstOrDefaultAsync();

        return entry;
    }
}
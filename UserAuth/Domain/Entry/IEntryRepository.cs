using Microsoft.EntityFrameworkCore;
using UserAuth.Domain.Entry;

namespace UserAuth.Domain.Entry;

public interface IEntryRepository
{
    void Add(Entry entry);
    Task<Entry?> GetByEmail(string email);
    Task<Entry?> GetById(Guid id);
    //Task<Entry?> GetByUsername(string username);
}
using Microsoft.EntityFrameworkCore;
using UserAuth.Domain.Rooms;
using UserAuth.Infrastructure.Data;

namespace UserAuth.Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _context;

    public RoomRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Room room)
    {
        _context.Set<Room>().Add(room);
        _context.SaveChanges();
    }

    public void Update(Room room)
    {
        _context.Set<Room>().Update(room);
        _context.SaveChanges();
    }

    public void Remove(Room room)
    {
        _context.Set<Room>().Remove(room);
        _context.SaveChanges();
    }

    public async Task<Room?> GetById(long id)
    {
        return await _context.Set<Room>().FindAsync(id);
    }

    public async Task<Room?> GetByIdWithMembers(long id)
    {
        var room = await _context.Set<Room>()
            .Include(room => room.Members)
            .ThenInclude(member => member.User)
            .FirstOrDefaultAsync(room => room.Id == id);
        return room;
    }
}
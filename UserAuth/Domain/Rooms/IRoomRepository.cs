namespace UserAuth.Domain.Rooms;

public interface IRoomRepository
{
    void Add(Room room);
    void Update(Room room);
    void Remove(Room room);
    Task<Room?> GetById(long id);
    Task<Room?> GetByIdWithMembers(long id);
}  
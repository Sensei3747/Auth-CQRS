using UserAuth.Domain.Entry;

namespace UserAuth.Domain.Rooms;

public class Member
{
    private Member(long userId, long roomId, Role role, DateTime joinedOn)
    {
        UserId = userId;
        RoomId = roomId;
        Role = role;
        JoinedOn = joinedOn;
    }
    private Member() { }

    public long Id { get; private set; }
    public long UserId { get; private set; }
    public long RoomId { get; private set; }
    public Entry.Entry User { get; private set; } // Assuming Entry is a class named 'Entry' inside the 'Entry' namespace
    public Role Role { get; private set; }
    public DateTime JoinedOn { get; private set; }

    public static Member Create(long userId, long roomId, Role role, DateTime joinedOn)
    {
        var member = new Member(userId, roomId, role, joinedOn);

        return member;
    }

}
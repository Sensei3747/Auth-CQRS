namespace UserAuth.Domain.Entry;
public sealed class Entry
{
    private Entry(string name, string email, string passwordHash, DateTime createdOn)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        CreatedOn = createdOn;
    }

    private Entry()
    {
    }

    public long Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public DateTime CreatedOn { get; private set; }

    public static Entry Create(string name, string email, string passwordHash, DateTime createdOn)
    {
        var user = new Entry(name, email, passwordHash, createdOn);

        return user;
    }
}
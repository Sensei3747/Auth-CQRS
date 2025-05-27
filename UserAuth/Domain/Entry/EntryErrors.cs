using UserAuth.Domain.Abstractions;

namespace UserAuth.Domain.Users;
public static class EntryErrors
{
    public static Error AlreadyExists = Error.Conflict(
        "User.AlreadyExists",
        "User with provided email already exists");

    public static Error NotFound = Error.NotFound(
        "User.Found",
        "The user with the specified identifier was not found");

    public static Error InvalidCredentials = Error.Conflict(
        "User.InvalidCredentials",
        "The provided credentials were invalid");

}
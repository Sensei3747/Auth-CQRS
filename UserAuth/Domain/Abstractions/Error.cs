namespace UserAuth.Domain.Abstractions;

public record Error(string name, string message, HttpResponseCodes code)
{
    public static Error None = new(string.Empty, string.Empty, HttpResponseCodes.InternalServerError);
    public static Error NullValue = new("NullError", "Null value encountered", HttpResponseCodes.BadRequest);

    public static Error InternalServerError(string name, string message) => new(name, message, HttpResponseCodes.InternalServerError);

    public static Error BadRequest(string name, string message) => new(name, message,
    HttpResponseCodes.BadRequest);

    public static Error NotFound(string name, string message) => new(name, message, HttpResponseCodes.NotFound);

    public static Error Conflict(string name, string message) => new(name, message, HttpResponseCodes.Conflict);
}

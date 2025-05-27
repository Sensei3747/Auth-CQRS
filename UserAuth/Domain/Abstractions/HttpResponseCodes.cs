namespace UserAuth.Domain.Abstractions;

public enum HttpResponseCodes
{
    BadRequest = 400,
    NotFound = 404,
    Conflict = 409,
    InternalServerError = 500,
}
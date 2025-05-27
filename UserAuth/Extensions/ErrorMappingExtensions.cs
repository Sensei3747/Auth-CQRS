using UserAuth.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace UserAuth.Extensions;

public static class ErrorMappingExtensions
{
    public static IActionResult ToActionResult(this Error error)
    {
        return error.code switch
        {
            HttpResponseCodes.NotFound => new NotFoundObjectResult(error),
            HttpResponseCodes.BadRequest => new BadRequestObjectResult(error),
            HttpResponseCodes.Conflict => new ConflictObjectResult(error),
            HttpResponseCodes.InternalServerError => new ObjectResult(error)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            },
            _ => new ObjectResult(error) { StatusCode = StatusCodes.Status500InternalServerError }
        };
    }
}
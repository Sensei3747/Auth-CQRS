using UserAuth.Application.Abstractions.Clock;

namespace UserAuth.Infrastructure.Clock;
internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
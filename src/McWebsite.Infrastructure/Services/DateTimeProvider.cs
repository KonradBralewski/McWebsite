using McWebsite.Application.Common.Interfaces.Services;

namespace McWebsite.Infrastructure.Services
{
    internal sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}

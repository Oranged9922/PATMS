using Application.Interfaces.Services;

namespace Infrastructure.Services
{

    /// <summary>
    /// A static class that provides access to the current date and time.
    /// </summary>
    public static class DateTimeProvider
    {
        private static IDateTimeProvider _provider;

        static DateTimeProvider()
        {
            _provider = new RealDateTimeProvider();
        }

        /// <summary>
        /// Sets the provider used to retrieve the current date and time.
        /// </summary>
        /// <param name="provider">The provider to use.</param>
        public static void SetProvider(IDateTimeProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// Gets the current date and time in the local time zone.
        /// </summary>
        /// <returns>The current date and time.</returns>
        public static DateTime Now()
        {
            return _provider.Now();
        }

        /// <summary>
        /// Gets the current date in the local time zone.
        /// </summary>
        /// <returns>The current date.</returns>
        public static DateTime Today()
        {
            return _provider.Now().Date;
        }

        /// <summary>
        /// Gets the current date and time in UTC.
        /// </summary>
        /// <returns>The current date and time in UTC.</returns>
        public static DateTime UtcNow()
        {
            return _provider.UtcNow();
        }
    }
    /// <summary>
    /// Provides access to a fixed date and time.
    /// </summary>
    public class FakeDateTimeProvider : IDateTimeProvider
    {
        private DateTime _dateTime;

        /// <summary>
        /// Initializes a new instance of the FakeDateTimeProvider class with the specified date and time.
        /// </summary>
        /// <param name="dateTime">The date and time to use.</param>
        public FakeDateTimeProvider(DateTime dateTime)
        {
            _dateTime = dateTime;
        }

        /// <summary>
        /// Gets the fixed date and time.
        /// </summary>
        /// <returns>The fixed date and time.</returns>
        public DateTime Now()
        {
            return _dateTime;
        }

        /// <summary>
        /// Gets the fixed date and time in UTC.
        /// </summary>
        /// <returns>The fixed date and time in UTC.</returns>
        public DateTime UtcNow()
        {
            return _dateTime.ToUniversalTime();
        }
    }

    /// <summary>
    /// Provides access to the current date and time using the system clock.
    /// </summary>
    public class RealDateTimeProvider : IDateTimeProvider
    {
        /// <summary>
        /// Gets the current date and time in the local time zone using the system clock.
        /// </summary>
        /// <returns>The current date and time.</returns>
        public DateTime Now()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// Gets the current date and time in UTC using the system clock.
        /// </summary>
        /// <returns>The current date and time in UTC.</returns>
        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}

namespace Application.Interfaces.Services
{
    /// <summary>
    /// Provides access to the current date and time.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Gets the current date and time in the local time zone.
        /// </summary>
        /// <returns>The current date and time.</returns>
        DateTime Now();

        /// <summary>
        /// Gets the current date and time in UTC.
        /// </summary>
        /// <returns>The current date and time in UTC.</returns>
        DateTime UtcNow();
    }
}

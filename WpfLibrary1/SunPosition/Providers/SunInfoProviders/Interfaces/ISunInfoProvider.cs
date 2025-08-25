namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Provides sunrise and sunset times for a given geographical location and date.
    /// </summary>
    /// <remarks>
    /// Implementations are expected to be configured with a fixed location (latitude/longitude)
    /// and timezone during construction. Method calls should return results for that configured location.
    /// </remarks>
    public interface ISunInfoProvider
    {
        /// <summary>
        /// Gets sunrise time.
        /// </summary>
        DateTime GetSunrise();

        /// <summary>
        /// Gets sunset time.
        /// </summary>
        DateTime GetSunset();
    }
}

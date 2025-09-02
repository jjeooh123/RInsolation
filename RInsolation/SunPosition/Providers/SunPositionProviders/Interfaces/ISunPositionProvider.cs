namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Provides sun position calculations for a given time.
    /// </summary>
    /// <remarks>
    /// The calculations occur at the specified <paramref name="time"/> in <see cref="GetPosition"/>.  
    /// The day of calculation (or base date) is established when the provider instance is created, 
    /// typically via constructor parameters.
    /// </remarks>
    public interface ISunPositionProvider
    {
        /// <summary>
        /// Gets the sun's position for the specified time.
        /// </summary>
        /// <param name="time">The time to calculate the position for.</param>
        SunPosition GetPosition(DateTime time);
    }
}

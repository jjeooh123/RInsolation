namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Represents the sun position calculation module, providing access to computed <see cref="SunPosition"/> data.
    /// </summary>
    public interface ISunPositionService
    {
        /// <summary>
        /// Calculates and returns a collection of sun positions for the configured date, position and frequency.
        /// </summary>
        IEnumerable<SunPosition> GetSunPositions();
    }
}

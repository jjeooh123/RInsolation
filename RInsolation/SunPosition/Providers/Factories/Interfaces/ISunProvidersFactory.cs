namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Factory interface for creating service providers capable of:
    /// - Calculating sunrise and sunset times (<see cref="ISunInfoProvider"/>)
    /// - Calculating the sun's position (<see cref="ISunPositionProvider"/>)
    /// </summary>
    public interface ISunProvidersFactory
    {
        /// <summary>
        /// Creates a provider that calculates sunrise and sunset times.
        /// </summary>
        ISunInfoProvider CreateSunInfoProvider();

        /// <summary>
        /// Creates a provider that calculates the sun's position.
        /// </summary>
        ISunPositionProvider CreateSunPositionProvider();
    }
}

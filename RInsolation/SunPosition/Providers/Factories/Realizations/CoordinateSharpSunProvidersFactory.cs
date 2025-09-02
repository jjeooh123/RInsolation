using Insolation.Config;

namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Concrete <see cref="ISunProvidersFactory"/> implementation using the CoordinateSharp NuGet package.
    /// </summary>
    public class CoordinateSharpSunProvidersFactory : ISunProvidersFactory
    {
        private readonly Configuration configuration;

        /// <summary>
        /// Initializes a new instance with the given configuration.
        /// </summary>
        public CoordinateSharpSunProvidersFactory(Configuration configuration) => this.configuration = configuration;

        /// <inheritdoc />
        public ISunInfoProvider CreateSunInfoProvider()
            => new CoordinateSharpSunInfoProvider(configuration.Latitude,
                                                  configuration.Longitude,
                                                  configuration.TimeZone,
                                                  configuration.CalcDay);

        /// <inheritdoc />
        public ISunPositionProvider CreateSunPositionProvider()
            => new CoordinateSharpSunPositionProvider(configuration.Latitude,
                                                        configuration.Longitude,
                                                        configuration.TimeZone);
    }
}

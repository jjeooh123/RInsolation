using Autodesk.Revit.DB;
using Insolation.Config;

namespace Insolation.NS_SunPosition
{
    // implementation of ISunProvidersFactory using built-in revit capabilities
    public class RevitSunProvidersFactory : ISunProvidersFactory
    {
        private readonly Configuration configuration;
        private readonly Document document;

        /// <summary>
        /// Initializes a new instance with the given configuration and document
        /// </summary>
        public RevitSunProvidersFactory(Configuration configuration, Document document)
        {
            this.configuration = configuration;
            this.document = document;
        }

        /// <inheritdoc />
        public ISunInfoProvider CreateSunInfoProvider()
            => new RevitSunInfoProvider(configuration.Latitude,
                                        configuration.Longitude,
                                        configuration.TimeZone,
                                        configuration.CalcDay,
                                        document);

        /// <inheritdoc />
        public ISunPositionProvider CreateSunPositionProvider()
            => new RevitSunPositionProvider(configuration.Latitude,
                                              configuration.Longitude,
                                              configuration.TimeZone,
                                              document);
    }
}

using Autodesk.Revit.DB;
using Insolation.Config;
using Insolation.XYZExtractor;

namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Default implementation of <see cref="ISunPositionServiceFactory"/>.
    /// Creates <see cref="ISunPositionService"/> instances by:
    /// 1. creating <see cref="ISunProvidersFactory"/> based on configuration.
    /// 2. Creating <see cref="ISunInfoProvider"/> and <see cref="ISunPositionProvider"/>.
    /// 3. Constructing the <see cref="SunPositionService"/>.
    /// </summary>
    public class SunPositionServiceFactory : ISunPositionServiceFactory
    {
        private Configuration config;
        private Document doc;

        /// <summary>
        /// Creates new instance of <see cref="SunPositionServiceFactory"/>
        /// </summary>
        /// <param name="config">Configuration object.</param>
        /// <param name="doc">Active Revit document.</param>
        public SunPositionServiceFactory(Configuration config, Document doc)
        {
            this.config = config;
            this.doc = doc;
        }

        /// <inheritdoc />
        public ISunPositionService Create()
        {
            DateTime calcDay = config.CalcDay;
            double GOSToffset = config.GOSToffset;
            double compFreq = config.CompFreq;

            // Resolve correct factory based on Configuration.CalcMethod
            ISunProvidersFactoryResolver sunProvidersFactoryResolver = new SunProvidersFactoryResolver(config, doc);
            var factory = sunProvidersFactoryResolver.Create();

            // Create providers
            ISunInfoProvider sunInfoProvider = factory.CreateSunInfoProvider();
            ISunPositionProvider sunPositionProvider = factory.CreateSunPositionProvider();

            // Create the service
            return new SunPositionService(calcDay, GOSToffset, compFreq, sunInfoProvider, sunPositionProvider);
        }
    }
}

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Insolation.Config;
using System.Windows.Controls;

namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Decorator for <see cref="ISunPositionServiceFactory"/> that allows overriding the computation frequency
    /// in the generated <see cref="Configuration"/>.
    /// </summary>
    /// <remarks>
    /// TODO: rename
    /// </remarks>
    public class SunPositionServiceFactoryForDrawLines : AutoSunPositionServiceFactory
    {
        private Document doc;
        private Configuration config;
        private readonly double fixedCompFreq;
         
        /// <summary>
        /// Initializes a new decorator instance.
        /// </summary>
        /// <param name="fixedCompFreq">The computation frequency override to apply.</param>
        public SunPositionServiceFactoryForDrawLines(double fixedCompFreq)
        {
            var globalContextManager = serviceProvider.GetIGlobalContextManager();

            this.fixedCompFreq = fixedCompFreq;
            doc = globalContextManager
                      .GetResult<ExternalCommandData>(SharedContextKeys.ExternalCommandData)
                      .Application.ActiveUIDocument.Document;

            config = globalContextManager.GetResult<Configuration>(SharedContextKeys.Configuration);

            innerFactory = new SunPositionServiceFactory(CloneWithOverride(config), doc);
        }

        /// <summary>
        /// Creates a modified copy of the given configuration with the overridden computation frequency.
        /// </summary>
        private Configuration CloneWithOverride(Configuration config)
            => new Configuration(config.Latitude,
                                     config.Longitude,
                                     config.TimeZone,
                                     config.CalcMethod,
                                     config.GOSToffset,
                                     config.CalcDay,
                                     fixedCompFreq);
    }
}

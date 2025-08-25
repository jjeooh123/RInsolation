using Autodesk.Revit.DB;
using Insolation.Config;

namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Decorator for <see cref="ISunPositionServiceFactory"/> that allows overriding the computation frequency
    /// in the generated <see cref="Configuration"/>.
    /// </summary>
    /// <remarks>
    /// TODO: rename
    /// </remarks>
    public class SunPositionServiceFactoryForDrawLines : ISunPositionServiceFactory
    {
        private readonly ISunPositionServiceFactory innerFactory;
        private readonly double fixedCompFreq;

        /// <summary>
        /// Initializes a new decorator instance.
        /// </summary>
        /// <param name="innerFactory">The underlying factory to wrap.</param>
        /// <param name="fixedCompFreq">The computation frequency override to apply.</param>
        public SunPositionServiceFactoryForDrawLines(ISunPositionServiceFactory innerFactory, double fixedCompFreq)
        {
            this.innerFactory = innerFactory;
            this.fixedCompFreq = fixedCompFreq;
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

        /// <inheritdoc />
        public ISunPositionService Create(Configuration config, Document doc)
            => innerFactory.Create(CloneWithOverride(config), doc);
    }
}

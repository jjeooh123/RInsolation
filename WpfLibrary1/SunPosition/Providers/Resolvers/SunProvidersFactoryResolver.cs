using Autodesk.Revit.DB;
using Insolation.Config;

namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Chooses the correct <see cref="ISunProvidersFactory"/> implementation based on <see cref="Configuration.CalcMethod"/>.
    /// </summary>
    public class SunProvidersFactoryResolver : ISunProvidersFactoryResolver
    {
        private readonly Configuration configuration;
        private readonly Document document;

        public SunProvidersFactoryResolver(Configuration configuration, Document document)
        {
            this.configuration = configuration;
            this.document = document;
        }

        /// <inheritdoc />
        public ISunProvidersFactory Create()
            => configuration.CalcMethod switch
                {
                    SunCoorCalcMethod.RevitSunAndShadowSettings => new RevitSunProvidersFactory(configuration, document),
                    SunCoorCalcMethod.CoordinateSharp => new CoordinateSharpSunProvidersFactory(configuration),
                    _ => throw new NotSupportedException($"Unsupported calculation method: {configuration.CalcMethod}")
                };
    }
}

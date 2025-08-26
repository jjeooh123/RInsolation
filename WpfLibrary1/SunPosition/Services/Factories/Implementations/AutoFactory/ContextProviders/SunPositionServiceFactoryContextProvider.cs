using Insolation.ElementExtractor;

namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Default implementation of <see cref="GlobalContextProviderBase"/>.
    /// Delegates all resolutions to base class, based on <see cref="ServiceLocator"/>.
    /// </summary>
    public class SunPositionServiceFactoryContextProvider : GlobalContextProviderBase;
}

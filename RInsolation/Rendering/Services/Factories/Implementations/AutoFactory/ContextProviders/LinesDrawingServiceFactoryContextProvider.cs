using Insolation.ElementExtractor;

namespace Insolation.Rendering
{
    /// <summary>
    /// Default implementation of <see cref="GlobalContextProviderBase"/>.
    /// Delegates all resolutions to base class, based on <see cref="ServiceLocator"/>.
    /// </summary>
    public class LinesDrawingServiceFactoryContextProvider : GlobalContextProviderBase;
}

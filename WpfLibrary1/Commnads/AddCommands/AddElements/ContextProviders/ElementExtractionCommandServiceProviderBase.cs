using Insolation.ElementExtractor;

namespace Insolation.Commnads
{

    /// <summary>
    /// Base service provider for <see cref="ElementExtractionCommandBase"/>.
    /// Encapsulates service resolution via a <see cref="ServiceLocator"/>.
    /// </summary>
    /// <remarks>
    /// This class acts as a bridge between commands and services.
    /// TODO: Consider refactoring.
    /// TODO: clearer naming.
    /// </remarks>
    public abstract class ElementExtractionCommandServiceProviderBase : IGlobalContextProvider,
                                                          IElementExtractorFactoryResolverProvider

    {
        public virtual IGlobalContextManager GetIGlobalContextManager() => ServiceLocator.Get<IGlobalContextManager>();
        public virtual IElementExtractorFactoryResolver GetElementExtractorFactoryResolver() => ServiceLocator.Get<IElementExtractorFactoryResolver>();
    }
}

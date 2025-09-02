using Insolation.ElementExtractor;

namespace Insolation.Commnads
{
    /// <summary>
    /// Service provider interface for retrieving element extractor factories.
    /// </summary>
    /// <remarks>
    /// TODO: Consider refactoring.
    /// TODO: clearer naming.
    /// </remarks>
    public interface IElementExtractorFactoryResolverProvider
    {
        public IElementExtractorFactoryResolver GetElementExtractorFactoryResolver();
    }
}

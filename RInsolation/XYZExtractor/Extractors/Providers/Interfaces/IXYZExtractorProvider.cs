namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Provides <see cref="IXYZExtractor"/> instances based on a given <see cref="XYZExtractionStrategy"/>.
    /// </summary>
    public interface IXYZExtractorProvider
    {
        /// <summary>
        /// Retrieves an extractor instance for the specified strategy.
        /// </summary>
        /// <param name="ExtractionStrategy">The extraction strategy.</param>
        /// <returns>The corresponding extractor instance.</returns>
        IXYZExtractor Get(XYZExtractionStrategy ExtractionStrategy);
    }
}

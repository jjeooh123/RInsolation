namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Factory interface for creating <see cref="IXYZExtractor"/> instances
    /// based on a specified <see cref="XYZExtractionStrategy"/>.
    /// </summary>
    public interface IXYZExtractorFactory
    {
        /// <summary>
        /// Creates an instance of an <see cref="IXYZExtractor"/> corresponding
        /// to the given <paramref name="ExtractionStrategy"/>.
        /// </summary>
        /// <param name="ExtractionStrategy">The strategy to determine which extractor to create.</param>
        /// <returns>An initialized <see cref="IXYZExtractor"/> instance.</returns>
        /// <remarks>
        /// TODO: Consider refactoring so that <c>Create()</c> has no parameters, 
        /// and the choice of extractor happens during factory construction 
        /// (possibly by using separate factory implementations for each strategy).
        /// </remarks>
        IXYZExtractor Create(XYZExtractionStrategy ExtractionStrategy); // TODO Create(void)?
    }
}

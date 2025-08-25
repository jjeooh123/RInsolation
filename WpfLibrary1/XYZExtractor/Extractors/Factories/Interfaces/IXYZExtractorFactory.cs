namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Factory interface for creating <see cref="IXYZExtractor"/> instances
    /// </summary>
    public interface IXYZExtractorFactory
    {
        /// <summary>
        /// Creates an instance of an <see cref="IXYZExtractor"/>.
        /// </summary>
        IXYZExtractor Create();
    }
}

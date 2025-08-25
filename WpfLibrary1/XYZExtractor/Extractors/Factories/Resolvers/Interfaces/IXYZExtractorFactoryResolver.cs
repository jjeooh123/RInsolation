namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Class encapsulating strategy-based factory selection and creation.
    /// </summary>
    /// <remarks>
    /// TODO: consider SRS?.
    /// </remarks>
    public interface IXYZExtractorFactoryResolver
    {
        /// <summary>
        /// Resolve XYZ Extractor Factory, based on given strategy(based on type of element).
        /// </summary>
        /// <param name="ExtractionStrategy">extraction strategy <see cref="XYZExtractionStrategy"/></param>
        /// <returns>Configurated <see cref="IXYZExtractorFactory"/></returns>
        public IXYZExtractorFactory Resolve(XYZExtractionStrategy ExtractionStrategy);
    }
}

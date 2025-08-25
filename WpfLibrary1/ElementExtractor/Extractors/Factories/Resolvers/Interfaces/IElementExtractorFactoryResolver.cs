namespace Insolation.ElementExtractor
{
    /// <summary>
    /// Class encapsulating strategy-based factory selection and creation(srs?).
    /// </summary>
    public interface IElementExtractorFactoryResolver
    {
        /// <summary>
        /// Resolve Element Extractor Factory, based on given strategy.
        /// </summary>
        /// <param name="strategy">extraction strategy <see cref="ElementExtractionStrategy"/></param>
        /// <returns>Configurated <see cref="IElementExtractoFactory"/></returns>
        /// <exception cref="NotSupportedException"></exception>
        public IElementExtractoFactory Resolve(ElementExtractionStrategy strategy);

    }
}

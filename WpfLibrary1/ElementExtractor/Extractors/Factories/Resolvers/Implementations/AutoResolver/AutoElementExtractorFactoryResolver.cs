using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Insolation.ElementExtractor
{
    /// <summary>
    /// Implemantation of <see cref="IElementExtractorFactoryResolver"/>,
    /// where active Revit documnet is taken from global context.
    /// </summary>
    public class AutoElementExtractorFactoryResolver : IElementExtractorFactoryResolver
    {
        private Document document;
        private ElementExtractorFactoryResolver innerFactoryResolver;

        /// <summary>
        /// Provides factories and shared services needed by the module.
        /// </summary>
        public virtual GlobalContextProviderBase serviceProvider { get; set; } = new DefaultContextProvider();

        /// <inheritdoc/>
        public IElementExtractoFactory Resolve(ElementExtractionStrategy strategy) 
        {
            document = serviceProvider.GetIGlobalContextManager()
                .GetResult<ExternalCommandData>(SharedContextKeys.ExternalCommandData)
                .Application.ActiveUIDocument.Document;

            innerFactoryResolver = new ElementExtractorFactoryResolver(document);
            return innerFactoryResolver.Resolve(strategy);
        }
    }
}

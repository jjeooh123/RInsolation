using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace Insolation.ElementExtractor
{
    /// <summary>
    /// An element extractor that returns all elements in the document
    /// belonging to the same family as the selected element.
    /// </summary>
    /// <remarks>
    /// TODO: make explicit contract that selection must contain only one element
    /// </remarks>
    public class ElementExtractorByFamilyName : IElementExtractor
    {
        private readonly Document document;

        /// <summary>
        /// Initializes a new instance of <see cref="ElementExtractorByFamilyName"/>.
        /// </summary>
        /// <param name="document">The active Revit document.</param>
        public ElementExtractorByFamilyName(Document document) => this.document = document;

        public IEnumerable<ElementId> ExtractElements(Selection selection)
        {
            // get first selected element 
            /// TODO: make explicit contract that selection must contain only one element
            ElementId elementId = selection.GetElementIds().FirstOrDefault();
            Element element = document.GetElement(elementId);

            // get family name of selected element
            string param = element.get_Parameter(BuiltInParameter.ELEM_FAMILY_PARAM).AsValueString();

            // collect all elements with same family name
            return new FilteredElementCollector(document).OfCategory(element.Category.BuiltInCategory)
                                                         .WhereElementIsNotElementType()
                                                         .Where(e =>
                                                         {
                                                             string c_param = e.get_Parameter(BuiltInParameter.ELEM_FAMILY_PARAM).AsValueString();
                                                             return c_param == param;
                                                         }).Select(e => e.Id).ToList();
        }
    }
}

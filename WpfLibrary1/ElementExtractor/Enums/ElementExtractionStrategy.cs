namespace Insolation.ElementExtractor
{
    /// <summary>
    /// Defines available strategies for extracting elements from the active revit document.
    /// </summary>
    /// <remarks>
    /// TODO: Consider auto-registration via reflection or DI container
    /// to simplify adding new strategies without changing factory code.
    /// </remarks>
    public enum ElementExtractionStrategy
    {
        /// <summary>
        /// Extracts elements directly from the current selection.
        /// </summary>
        BySelection,

        /// <summary>
        /// Extracts elements that share the same family name as the selected element.
        /// </summary>
        ByFamilyName
    }
}

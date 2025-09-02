namespace Insolation.XYZExtractor
{
    // Defines the set of available strategies for extracting an XYZ point from an element.
    // This enumeration is used by factories and providers to determine which extractor
    // implementation should be used at runtime.
    public enum XYZExtractionStrategy
    {
        WindowXYZExtractor,
        DefaultXYZExtractor
    }
}
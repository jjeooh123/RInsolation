namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Encapsulates logic for selecting and instantiating an <see cref="ISunProvidersFactory"/> implementation
    /// based on runtime configuration or context.
    /// </summary>
    public interface ISunProvidersFactoryResolver
    {
        /// <summary>
        /// Creates the appropriate <see cref="ISunProvidersFactory"/> for the current environment.
        /// </summary>
        ISunProvidersFactory Create();
    }
}

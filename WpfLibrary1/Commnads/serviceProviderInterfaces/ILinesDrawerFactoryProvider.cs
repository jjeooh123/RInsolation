using Insolation.Rendering;

namespace Insolation.Commnads
{
    /// <summary>
    /// Service provider interface for retrieving line drawing service factories.
    /// </summary>
    /// <remarks>
    /// TODO: Consider refactoring.
    /// TODO: clearer naming.
    /// </remarks>
    public interface ILinesDrawerFactoryProvider
    {
        public ILinesDrawingServiceFactory GetLinesDrawingServiceFactory();
    }
}

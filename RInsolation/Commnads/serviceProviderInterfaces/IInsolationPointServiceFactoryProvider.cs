using Insolation.XYZExtractor;

namespace Insolation.Commnads
{
    /// <summary>
    /// Service provider interface for retrieving insolation point service factories.
    /// </summary>
    /// <remarks>
    /// TODO: Consider refactoring.
    /// TODO: clearer naming.
    /// </remarks>
    public interface IInsolationPointServiceFactoryProvider
    {
        public IInsolationPointServiceFactory GetInsolationPointServiceFactory();
    }
}

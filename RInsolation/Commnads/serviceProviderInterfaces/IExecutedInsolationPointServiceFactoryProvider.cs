using Insolation.InsolationCalculator;

namespace Insolation.Commnads
{
    /// <summary>
    /// Service provider interface for retrieving factories that create executed insolation point services.
    /// </summary>
    /// <remarks>
    /// TODO: Consider refactoring.
    /// TODO: clearer naming.
    /// </remarks>
    public interface IExecutedInsolationPointServiceFactoryProvider
    {
        public IExecutedInsolationPointServiceFactory GetExecutedInsolationPointServiceFactory();
    }
}

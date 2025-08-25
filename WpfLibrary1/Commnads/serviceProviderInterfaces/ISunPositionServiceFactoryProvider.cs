using Insolation.NS_SunPosition;

namespace Insolation.Commnads
{
    /// <summary>
    /// Service provider interface for retrieving sun position service factories.
    /// </summary>
    /// <remarks>
    /// TODO: Consider refactoring.
    /// TODO: clearer naming.
    /// </remarks>
    public interface ISunPositionServiceFactoryProvider
    {
        public ISunPositionServiceFactory GetSunPositionServiceFactory();
    }
}

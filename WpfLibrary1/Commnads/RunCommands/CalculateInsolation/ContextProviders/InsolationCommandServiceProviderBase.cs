using Insolation.InsolationCalculator;
using Insolation.NS_SunPosition;
using Insolation.XYZExtractor;

namespace Insolation.Commnads
{
    /// <summary>
    /// Base service provider for <see cref="CalculateInsolationCommand"/>.
    /// Encapsulates service resolution via <see cref="ServiceLocator"/>.
    /// </summary>
    /// <remarks>
    /// This class acts as a bridge between commands and services.
    /// TODO: Consider refactoring.
    /// TODO: clearer naming.
    /// </remarks>
    public abstract class InsolationCommandServiceProviderBase : IGlobalContextProvider,
                                                                 ISunPositionServiceFactoryProvider,
                                                                 IExecutedInsolationPointServiceFactoryProvider,
                                                                 IInsolationPointServiceFactoryProvider
    {
        public virtual IGlobalContextManager GetIGlobalContextManager() => ServiceLocator.Get<IGlobalContextManager>();

        public virtual ISunPositionServiceFactory GetSunPositionServiceFactory()
            => ServiceLocator.Get<ISunPositionServiceFactory>("SunPositionServiceFactory");

        public virtual IExecutedInsolationPointServiceFactory GetExecutedInsolationPointServiceFactory()
            => ServiceLocator.Get<IExecutedInsolationPointServiceFactory>();

        public virtual IInsolationPointServiceFactory GetInsolationPointServiceFactory()
            => ServiceLocator.Get<IInsolationPointServiceFactory>();
    }
}

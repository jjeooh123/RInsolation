using Insolation.NS_SunPosition;
using Insolation.Rendering;
using Insolation.XYZExtractor;

namespace Insolation.Commnads
{
    /// <summary>
    /// Base service provider for <see cref="DrawSunLinesCommand"/>.
    /// Encapsulates service resolution via <see cref="ServiceLocator"/>.
    /// </summary>
    /// <remarks>
    /// This class acts as a bridge between commands and services.
    /// TODO: Consider refactoring.
    /// TODO: clearer naming.
    /// </remarks>
    public abstract class DrawSunLinesCommandServiceProviderBase : IGlobalContextProvider,
                                                            ISunPositionServiceFactoryProvider,
                                                            IInsolationPointServiceFactoryProvider,
                                                            ILinesDrawerFactoryProvider
    {
        public virtual ILinesDrawingServiceFactory GetLinesDrawingServiceFactory()
            => ServiceLocator.Get<ILinesDrawingServiceFactory>();

        public virtual IGlobalContextManager GetIGlobalContextManager() 
            => ServiceLocator.Get<IGlobalContextManager>();

        public virtual ISunPositionServiceFactory GetSunPositionServiceFactory()
            => ServiceLocator.Get<ISunPositionServiceFactory>("SunPositionServiceFactoryForDrawLines");

        public virtual IInsolationPointServiceFactory GetInsolationPointServiceFactory()
            => ServiceLocator.Get<IInsolationPointServiceFactory>();
    }
}

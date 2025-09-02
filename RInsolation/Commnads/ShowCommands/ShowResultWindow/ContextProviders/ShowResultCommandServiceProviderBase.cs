namespace Insolation.Commnads
{
    /// <summary>
    /// Base service provider for <see cref="ShowResultCommand"/>.
    /// Encapsulates service resolution via <see cref="ServiceLocator"/>.
    /// </summary>
    /// <remarks>
    /// This class acts as a bridge between commands and services.
    /// TODO: Consider refactoring.
    /// TODO: clearer naming.
    /// </remarks>
    public abstract class ShowResultCommandServiceProviderBase : IGlobalContextProvider
    {
        public virtual IGlobalContextManager GetIGlobalContextManager() => ServiceLocator.Get<IGlobalContextManager>();
    }
}

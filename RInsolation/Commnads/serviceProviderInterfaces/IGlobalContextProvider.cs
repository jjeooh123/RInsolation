namespace Insolation.Commnads
{
    /// <summary>
    /// Service provider interface for accessing the global context manager.
    /// </summary>
    /// <remarks>
    /// TODO: Consider refactoring.
    /// TODO: clearer naming.
    /// </remarks>
    public interface IGlobalContextProvider
    {
        public IGlobalContextManager GetIGlobalContextManager();
    }
}

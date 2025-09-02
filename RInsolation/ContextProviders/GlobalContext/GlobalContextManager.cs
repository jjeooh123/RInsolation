namespace Insolation
{
    /// <summary>
    /// Default implementation of <see cref="IGlobalContextManager"/>.
    /// Wraps <see cref="SharedContext"/> for storing global state.
    /// </summary>
    /// <remarks>
    /// TODO: Consider unifying with <see cref="ServiceLocator"/> to avoid overlap.
    /// </remarks>
    public class GlobalContextManager : IGlobalContextManager
    {
        private readonly SharedContext context = new();

        /// <inheritdoc />
        public void SubmitResult(string key, object result) => context.Set(key, result);

        /// <inheritdoc />
        public T GetResult<T>(string key) => context.Get<T>(key);
    }
}

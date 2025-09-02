namespace Insolation
{
    /// <summary>
    /// Provides an interface for managing global context across commands.
    /// </summary>
    /// <remarks>
    /// Used to store and retrieve shared results between different parts of the module.
    /// </remarks>
    public interface IGlobalContextManager
    {
        /// <summary>
        /// Submits a result value into the global context.
        /// </summary>
        /// <param name="key">The key under which to store the result.</param>
        /// <param name="value">The result value.</param>
        public void SubmitResult(string key, object value);

        /// <summary>
        /// Retrieves a result from the global context by key.
        /// </summary>
        /// <typeparam name="T">Expected type of the result.</typeparam>
        /// <param name="key">The key associated with the result.</param>
        /// <returns>The stored value.</returns>
        public T GetResult<T>(string key);
    }
}

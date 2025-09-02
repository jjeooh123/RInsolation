namespace Insolation
{
    /// <summary>
    /// A simple generic service locator for registering and retrieving services.
    /// </summary>
    /// <remarks>
    /// Supports optional named registrations for multiple implementations of the same type.
    /// </remarks>
    public static class ServiceLocator
    {
        private static readonly Dictionary<(Type, String?), object> services = new();

        /// <summary>
        /// Registers a service instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="service">The service instance to register.</param>
        /// <param name="name">An optional name for distinguishing multiple services of the same type.</param>
        public static void Register<T>(T service, String? name = null) where T : class
        {
            var key = (typeof(T), name);
            services[key] = service;

        }

        /// <summary>
        /// Retrieves a registered service of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="name">The optional name if the service was registered with one.</param>
        /// <returns>The requested service.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the service is not registered.
        /// </exception>
        public static T Get<T>(String? name = null) where T : class
        {
            var key = (typeof(T), name);
            if (services.TryGetValue(key, out var service))
                return (T)service;

            throw new InvalidOperationException($"Service of type {typeof(T)} with name '{name}' is not registered.");
        }
    }
}

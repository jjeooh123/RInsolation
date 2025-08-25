namespace Insolation
{
    /// <summary>
    /// Defines shared keys for context values.
    /// </summary>
    /// <remarks>
    /// TODO: replace string constants with a better alternative.
    /// </remarks>
    public static class SharedContextKeys
    {
        public const string Configuration = "Configuration";
        public const string ElementIds = "ElementIds";
        public const string ExecutedInsolation = "ExecutedInsolation";
        public const string CreatedElementsId = "CreatedElementsId";
    }

    /// <summary>
    /// A module for storing and retrieving arbitrary key-value data.
    /// </summary>
    public class SharedContext
    {
        private readonly Dictionary<string, object> data = new();

        /// <summary>
        /// Stores a value under the specified key.
        /// </summary>
        public void Set<T>(string key, T value) => data[key] = value;

        /// <summary>
        /// Retrieves a value by key, or default if not present.
        /// </summary>
        public T Get<T>(string key) => data.TryGetValue(key, out var value) ? (T)value : default;

        //public bool Contains(string key) => data.ContainsKey(key);
    }
}

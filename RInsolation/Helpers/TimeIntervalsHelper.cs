namespace Insolation.Helpers
{
    /// <summary>
    /// Provides formatting utilities for collections of time intervals.
    /// </summary>
    public static class TimeIntervalsHelper
    {
        /// <summary>
        /// Converts a collection of <see cref="DateTime"/> values into a formatted string
        /// representing time ranges in "HH:mm-HH:mm" format.
        /// </summary>
        /// <remarks>
        /// Assumes the collection represents paired start and end times.
        /// Example: [08:00, 09:30, 13:00, 15:00] → "08:00-09:30 13:00-15:00".
        /// </remarks>
        /// <param name="intervals">The ordered sequence of time intervals.</param>
        /// <returns>A formatted string of time ranges, or an empty string if no intervals exist.</returns>
        public static string TimeIntervalsToString(IEnumerable<DateTime> intervals)
        {
            string result = "";
            if (intervals.Count() == 0) return "";
            for (int i = 0; i < intervals.Count() - 1; i += 2)
                result += intervals.ElementAt(i).ToString("HH:mm") + "-" + intervals.ElementAt(i + 1).ToString("HH:mm") + " ";
            return result;
        }
    }
}

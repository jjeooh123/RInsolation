namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Helper class for generating evenly spaced time intervals between two points in time.
    /// </summary>
    public static class TimeIntervalHelper
    {
        /// <summary>
        /// Generates a list of DateTime values from <paramref name="start"/> to <paramref name="end"/>
        /// at intervals of <paramref name="dtMinutes"/> minutes.
        /// </summary>
        /// <param name="start">The start time of the interval range.</param>
        /// <param name="end">The end time of the interval range.</param>
        /// <param name="dtMinutes">The interval size in minutes.</param>
        /// <returns>A sequence of DateTime values, including <paramref name="start"/> and <paramref name="end"/>.</returns>
        public static IEnumerable<DateTime> GenerateTimeIntervals(DateTime start, DateTime end, double dtMinutes)
        {
            List<DateTime> result = new List<DateTime>();
            var currentTime = start;

            while (currentTime < end) 
            {
                result.Add(currentTime);
                currentTime = currentTime.AddMinutes(dtMinutes);
            }
            result.Add(end);

            return result;
        }
    }
}
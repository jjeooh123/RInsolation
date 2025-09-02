namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Provides sun position data for a given day, offset, and computation frequency.
    /// </summary>
    public class SunPositionService : ISunPositionService
    {
        private readonly ISunInfoProvider sunInfoProvider;
        private readonly ISunPositionProvider sunPositionProvider;

        private readonly DateTime calcDay;
        private readonly double GOSToffset;
        private readonly double compFreq;

        /// <summary>
        /// Initializes a new instance of <see cref="SunPositionService"/>.
        /// </summary>
        /// <param name="calcDay">The day for which sun positions will be calculated.</param>
        /// <param name="GOSToffset">Offset applied to sunrise-sunset interval.</param>
        /// <param name="compFreq">Computation frequency in minutes.</param>
        /// <param name="sunInfoProvider">Provider for sunrise/sunset information.</param>
        /// <param name="sunPositionProvider">Provider for sun position calculations.</param>
        public SunPositionService(DateTime calcDay,
                                  double GOSToffset,
                                  double compFreq,
                                  ISunInfoProvider sunInfoProvider,
                                  ISunPositionProvider sunPositionProvider)
        {
            this.calcDay = calcDay;
            this.GOSToffset = GOSToffset;
            this.compFreq = compFreq;

            this.sunInfoProvider = sunInfoProvider;
            this.sunPositionProvider = sunPositionProvider;
        }

        /// <inheritdoc />
        public IEnumerable<SunPosition> GetSunPositions()
        {
            // Determine calculation window
            var sunrise = sunInfoProvider.GetSunrise();
            var sunset = sunInfoProvider.GetSunset();
            var calcStart = sunrise.AddHours(GOSToffset);
            var calcEnd = sunset.AddHours(-GOSToffset);

            // Generate time intervals
            IEnumerable<DateTime> times = TimeIntervalHelper.GenerateTimeIntervals(calcStart, calcEnd, compFreq);

            // Calculate sun position for each time
            List<SunPosition> result = new List<SunPosition>();
            foreach (var time in times) result.Add(sunPositionProvider.GetPosition(time));
            return result;
        }
    }
}

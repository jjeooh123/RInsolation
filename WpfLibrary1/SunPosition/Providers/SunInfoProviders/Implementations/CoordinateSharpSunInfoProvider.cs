using CoordinateSharp;

namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Concrete <see cref="ISunInfoProvider"/> implementation using the CoordinateSharp NuGet package.
    /// </summary>
    public class CoordinateSharpSunInfoProvider : ISunInfoProvider
    {
        private readonly double latitudeDegrees;
        private readonly double longitudeDegrees;
        private readonly double timeZone;
        private readonly DateTime calcDay;
        private Coordinate c;

        /// <summary>
        /// Creates a new CoordinateSharp-based sunrise/sunset calculation provider.
        /// </summary>
        /// <param name="latitudeDegrees">Latitude in decimal degrees.</param>
        /// <param name="longitudeDegrees">Longitude in decimal degrees.</param>
        /// <param name="timeZone">Time zone (UTC).</param>
        /// <param name="calcDay">The day for which sunrise/sunset is calculated.</param>
        public CoordinateSharpSunInfoProvider(double latitudeDegrees,
                                         double longitudeDegrees,
                                         double timeZone,
                                         DateTime calcDay)
        {
            this.latitudeDegrees = latitudeDegrees;
            this.longitudeDegrees = longitudeDegrees;
            this.timeZone = timeZone;
            this.calcDay = calcDay;
            c = SetLocation();
        }

        /// <summary>
        /// Initializes and configures a CoordinateSharp <see cref="Coordinate"/> instance.
        /// </summary>
        private Coordinate SetLocation()
        {
            var c = new Coordinate(latitudeDegrees, longitudeDegrees, calcDay);
            c.Offset = timeZone;
            return c;
        }

        /// <inheritdoc />
        public DateTime GetSunrise() => (DateTime)c.CelestialInfo.SunRise;

        /// <inheritdoc />
        public DateTime GetSunset() => (DateTime)c.CelestialInfo.SunSet;
    }
}

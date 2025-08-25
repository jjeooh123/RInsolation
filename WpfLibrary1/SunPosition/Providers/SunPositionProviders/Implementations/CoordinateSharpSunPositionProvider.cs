using CoordinateSharp;

namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Calculates the sun's position using the CoordinateSharp NuGet Package.
    /// </summary>
    public class CoordinateSharpSunPositionProvider : ISunPositionProvider
    {
        private readonly double latitudeDegrees;
        private readonly double longitudeDegrees;
        private readonly double timeZone;
        private Coordinate c;

        /// <summary>
        /// Initializes a new CoordinateSharp-based sun position calculation provider.
        /// </summary>
        /// <param name="latitudeDegrees">Latitude in decimal degrees.</param>
        /// <param name="longitudeDegrees">Longitude in decimal degrees.</param>
        /// <param name="timeZone">Time zone (UTC).</param>
        public CoordinateSharpSunPositionProvider(double latitudeDegrees,
                                                  double longitudeDegrees,
                                                  double timeZone)
        {
            this.latitudeDegrees = latitudeDegrees;
            this.longitudeDegrees = longitudeDegrees;
            this.timeZone = timeZone;
            c = SetLocation();
        }

        /// <summary>
        /// Configures the underlying <see cref="Coordinate"/> instance for this provider's location and time zone.
        /// </summary>
        private Coordinate SetLocation()
        {
            var c = new Coordinate(latitudeDegrees, longitudeDegrees);
            c.Offset = timeZone;
            return c;
        }

        /// <inheritdoc />
        public SunPosition GetPosition(DateTime time)
        {
            // Update datetime for celestial calculations
            c.GeoDate = time;

            // Convert from azimuth/altitude to latitude/longitude
            var coordinate = CoordinateConverter.CelestialDToSphericalD(
                c.CelestialInfo.SunAzimuth,
                c.CelestialInfo.SunAltitude);

            return new SunPosition(coordinate.latitudeDegrees, coordinate.longitudeDegrees, time);
        }
    }
}

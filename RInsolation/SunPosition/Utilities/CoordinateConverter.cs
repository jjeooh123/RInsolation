namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Provides conversions between celestial (azimuth/altitude), spherical (latitude/longitude),
    /// and Cartesian (X, Y, Z) coordinate systems.
    /// </summary>
    public static class CoordinateConverter
    {
        /// <summary>
        /// Converts celestial coordinates (in radians) to spherical coordinates (in degrees).
        /// </summary>
        public static (double latitudeDegrees, double longitudeDegrees) CelestialRToSphericalD(double azimuthRadians, double altitudeRadians)
            => (90.0 - float.RadiansToDegrees((float)azimuthRadians), 90.0 - float.RadiansToDegrees((float)altitudeRadians));

        /// <summary>
        /// Converts celestial coordinates (in degrees) to spherical coordinates (in degrees).
        /// </summary>
        public static (double latitudeDegrees, double longitudeDegrees) CelestialDToSphericalD(double azimuthDegrees, double altitudeDegrees)
            => (90.0 - azimuthDegrees, 90.0 - altitudeDegrees);

        /// <summary>
        /// Converts spherical coordinates (in degrees) to Cartesian coordinates, given a radius.
        /// </summary>
        public static (double X, double Y, double Z) SpericalDToCartesian(double latitudeDegrees, double longitudeDegrees, double r)
            => (r * Math.Sin(float.DegreesToRadians((float)longitudeDegrees)) * Math.Cos(float.DegreesToRadians((float)latitudeDegrees)),
                r * Math.Sin(float.DegreesToRadians((float)longitudeDegrees)) * Math.Sin(float.DegreesToRadians((float)latitudeDegrees)),
                r * Math.Cos(float.DegreesToRadians((float)longitudeDegrees)));

        /// <summary>
        /// Converts spherical coordinates (in degrees) to Cartesian coordinates (normalized radius).
        /// </summary>
        public static (double X, double Y, double Z) SpericalDToCartesian(double latitudeDegrees, double longitudeDegrees)
            => (Math.Sin(float.DegreesToRadians((float)longitudeDegrees)) * Math.Cos(float.DegreesToRadians((float)latitudeDegrees)),
                Math.Sin(float.DegreesToRadians((float)longitudeDegrees)) * Math.Sin(float.DegreesToRadians((float)latitudeDegrees)),
                Math.Cos(float.DegreesToRadians((float)longitudeDegrees)));
    }
}

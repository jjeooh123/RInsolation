namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Representation of the sun's position using geographic coordinates and time.
    /// </summary>
    public readonly struct SunPosition
    {
        /// <summary>
        /// Gets the geographic longitude in decimal degrees.
        /// </summary>
        public double Longitude { get; }

        /// <summary>
        /// Gets the geographic latitude in decimal degrees.
        /// </summary>
        public double Latitude { get; }

        /// <summary>
        /// Gets the associated time
        /// </summary>
        public DateTime Time { get; }

        /// <summary>
        /// Creates a new instance of <see cref="SunPosition"/>.
        /// </summary>
        /// <param name="latitude">Latitude in decimal degrees.</param>
        /// <param name="longitude">Longitude in decimal degrees.</param>
        /// <param name="time">The time for which the position is calculated.</param>
        public SunPosition(double latitude, double longitude, DateTime time)
        {
            Latitude = latitude;
            Longitude = longitude;
            Time = time;
        }
    }

}

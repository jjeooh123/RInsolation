using Autodesk.Revit.DB;

namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Structure that associates a calculated <see cref="NS_SunPosition.SunPosition"/> (geographical and time-based data)
    /// with a specific Cartesian coordinate (<see cref="XYZ"/>).
    /// </summary>
    /// <remarks>
    /// TODO: The <see cref="SunCoordinate"/> contains both the <see cref="SunPosition"/> object
    /// (which itself holds latitude, longitude, and time) and the XYZ position.
    /// In many use cases, the full <see cref="SunPosition"/> may not be necessary, and a simpler structure could suffice.
    /// </remarks>
    public readonly struct SunCoordinate
    {
        /// <summary>
        /// Gets the sun's position information (latitude, longitude, time).
        /// </summary>
        public SunPosition SunPosition { get; }

        /// <summary>
        /// Gets the corresponding XYZ spatial coordinate in model space.
        /// </summary>
        public XYZ Position { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="SunCoordinate"/>.
        /// </summary>
        /// <param name="sunPosition">Sun position data.</param>
        /// <param name="position">Model coordinate corresponding to the sun position.</param>
        public SunCoordinate(SunPosition sunPosition, XYZ position)
        {
            SunPosition = sunPosition;
            Position = position;
        }
    }
}

using Autodesk.Revit.DB;
using System.Windows;

namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Calculates the sun's position using Autodesk Revit's SunAndShadowSettings.
    /// </summary>
    public class RevitSunPositionProvider : ISunPositionProvider
    {
        private readonly double latitudeDegrees;
        private readonly double longitudeDegrees;
        private readonly double timeZone;
        private readonly Document document;

        /// <summary>
        /// Creates a new Revit-based sun position calculation provider.
        /// </summary>
        /// <param name="latitudeDegrees">Latitude in decimal degrees.</param>
        /// <param name="longitudeDegrees">Longitude in decimal degrees.</param>
        /// <param name="timeZone">Time zone (UTC).</param>
        /// <param name="document">The active Revit document.</param>
        public RevitSunPositionProvider(double latitudeDegrees, double longitudeDegrees, double timeZone, Document document)
        {
            this.latitudeDegrees = latitudeDegrees;
            this.longitudeDegrees = longitudeDegrees;
            this.timeZone = timeZone;
            this.document = document;
        }

        /// <summary>
        /// Updates the active project's site location to match this provider's configured location.
        /// </summary>
        private void SetLocation()
        {
            var siteLocation = document.ActiveProjectLocation.GetSiteLocation();
            siteLocation.Latitude = float.DegreesToRadians((float)latitudeDegrees);
            siteLocation.Longitude = float.DegreesToRadians((float)longitudeDegrees);
            siteLocation.TimeZone = timeZone;
        }

        /// <inheritdoc />
        public SunPosition GetPosition(DateTime time)
        {
            // TODO: Consider injecting the relevant View via constructor instead of relying on ActiveView.
            SunAndShadowSettings sunSettings = document.ActiveView.SunAndShadowSettings;
            if (sunSettings == null)
                throw new NullReferenceException("ActiveView must contain SunAndShadowSettings");

            ProjectLocation projectLocation = document.ActiveProjectLocation;

            // Set location and time for the calculation
            SetLocation();
            sunSettings.StartDateAndTime = DateTime.SpecifyKind(time, DateTimeKind.Utc);

            // Convert from azimuth/altitude to latitude/longitude
            var coordinate = CoordinateConverter.CelestialRToSphericalD(sunSettings.GetFrameAzimuth(sunSettings.ActiveFrame),
                                                                      sunSettings.GetFrameAltitude(sunSettings.ActiveFrame));
            return new SunPosition(coordinate.latitudeDegrees, coordinate.longitudeDegrees, time);
        }
    }
}

using Autodesk.Revit.DB;

namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Sunrise and sunset time calculation provider using Autodesk Revit's <see cref="Autodesk.Revit.DB.SunAndShadowSettings"/>.
    /// </summary>
    public class RevitSunInfoProvider : ISunInfoProvider
    {
        private readonly double latitudeDegrees;
        private readonly double longitudeDegrees;
        private readonly double timeZone;
        private readonly DateTime calcDay;
        private readonly Document document;

        /// <summary>
        /// Creates a new Revit-based sunrise/sunset calculation provider.
        /// </summary>
        /// <param name="latitudeDegrees">Latitude in decimal degrees.</param>
        /// <param name="longitudeDegrees">Longitude in decimal degrees.</param>
        /// <param name="timeZone">Time zone (UTC).</param>
        /// <param name="calcDay">The day for which sunrise/sunset is calculated.</param>
        /// <param name="document">The active Revit document.</param>

        public RevitSunInfoProvider(double latitudeDegrees,
                               double longitudeDegrees,
                               double timeZone,
                               DateTime calcDay,
                               Document document)
        {
            this.latitudeDegrees = latitudeDegrees;
            this.longitudeDegrees = longitudeDegrees;
            this.timeZone = timeZone;
            this.calcDay = calcDay;
            this.document = document;
        }

        /// <summary>
        /// Updates the project's site location with the configured latitude, longitude, and time zone (UTC).
        /// The project site location is then used internally by Revit's <see cref="SunAndShadowSettings"/> 
        /// to calculate the correct coordinates and times for sun positioning.
        /// </summary>
        private void SetLocation()
        {
            var siteLocation = document.ActiveProjectLocation.GetSiteLocation();
            siteLocation.Latitude = float.DegreesToRadians((float)latitudeDegrees);
            siteLocation.Longitude = float.DegreesToRadians((float)longitudeDegrees);
            siteLocation.TimeZone = timeZone;
        }

        /// <inheritdoc />
        public DateTime GetSunrise()
        {
            // TODO: Inject the view into the constructor rather than relying on ActiveView.
            SunAndShadowSettings sunSettings = document.ActiveView.SunAndShadowSettings;
            if (sunSettings == null)
                throw new NullReferenceException("ActiveView must contain SunAndShadowSettings");
            SetLocation();
            return sunSettings.GetSunrise(DateTime.SpecifyKind(calcDay, DateTimeKind.Utc));
        }

        /// <inheritdoc />
        public DateTime GetSunset()
        {
            // TODO: Inject the view into the constructor rather than relying on ActiveView.
            SunAndShadowSettings sunSettings = document.ActiveView.SunAndShadowSettings;
            if (sunSettings == null)
                throw new NullReferenceException("ActiveView must contain SunAndShadowSettings");
            SetLocation();
            return sunSettings.GetSunset(DateTime.SpecifyKind(calcDay, DateTimeKind.Utc));
        }
    }
}

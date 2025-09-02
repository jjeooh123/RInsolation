namespace Insolation.Config
{
    /// <summary>
    /// Represents configuration parameters for insolation calculations.
    /// </summary>
    /// <remarks>
    /// Encapsulates geographical, temporal, and method-specific settings.
    /// </remarks>
    public class Configuration
    {
        private double latitude;
        private double longitude;
        private int timeZone; // UTC
        private SunCoorCalcMethod calcMethod; // sun position calculation methods
        private double gOSToffset; // offset from sunrise/sunset (GOST standard) (hours)
        private DateTime calcDay; // calculation day
        private double compFreq; // computation frequency (minutes)
        private int permissibleGap; // threshold (minutes) for treating insolation as continuous despite gap (one).

        /// <summary>
        /// Latitude of the calculation Location.
        /// </summary>
        public double Latitude { get => latitude; set => latitude = value; } // широта 

        /// <summary>
        /// Longitude of the calculation point.
        /// </summary>
        public double Longitude { get => longitude; set => longitude = value; } // долгота

        /// <summary>
        /// UTC time zone offset.
        /// </summary>
        public int TimeZone { get => timeZone; set => timeZone = value; }

        /// <summary>
        /// The method used to calculate sun position.
        /// </summary>
        public SunCoorCalcMethod CalcMethod { get => calcMethod; set => calcMethod = value; }

        /// <summary>
        /// Offset (in hours) from sunrise/sunset, according to GOST standard.
        /// </summary>
        public double GOSToffset { get => gOSToffset; set => gOSToffset = value; }

        /// <summary>
        /// The date for which insolation is calculated.
        /// </summary>
        public DateTime CalcDay { get => calcDay; set => calcDay = value; }

        /// <summary>
        /// Computation frequency in minutes (accuracy/resolution).
        /// </summary>
        public double CompFreq { get => compFreq; set => compFreq = value; }

        /// <summary>
        /// Maximum permissible gap (in minutes) where insolation is still considered continuous.
        /// </summary>
        public int PermissibleGap { get => permissibleGap; set => permissibleGap = value; }

        /// <summary>
        /// Initializes a new instance of <see cref="Configuration"/>.
        /// </summary>
        public Configuration(double latitude, 
                             double longitude, 
                             int timeZone, 
                             SunCoorCalcMethod calcMethod, 
                             double gOSToffset, 
                             DateTime calcDay, 
                             double compFreq)
        {
            this.latitude = latitude;
            this.longitude = longitude;
            this.timeZone = timeZone;
            this.calcMethod = calcMethod;
            this.gOSToffset = gOSToffset;
            this.calcDay = calcDay;
            this.compFreq = compFreq;
            this.permissibleGap = 10;
        }

        // TODO: discard, handle unconfigurrated exception (guardian class?).
        public Configuration() 
        {
            this.latitude = 55.4502;
            this.longitude = 37.3703;
            this.timeZone = 3;
            this.calcMethod = SunCoorCalcMethod.RevitSunAndShadowSettings;
            this.gOSToffset = 1;
            this.calcDay = new DateTime(2025,04,22);
            this.compFreq = 60;
            this.permissibleGap = 10;
        }
    }
}

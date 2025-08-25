using Insolation.Config;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Insolation.Commnads;

namespace Insolation.MVVMs
{
    /// <summary>
    /// ViewModel for application configuration, 
    /// providing data binding between UI controls and the <see cref="Configuration"/> model.
    /// Implements <see cref="INotifyPropertyChanged"/> to support property change notifications.
    /// </summary>
    public class ConfigurationViewModel : INotifyPropertyChanged
    {
        private Configuration config;

        /// <summary>
        /// Collection of supported time zones (UTC offsets).
        /// Ranges from -12 to +26 inclusive.
        /// </summary>
        public ObservableCollection<int> TimeZones { get; } = new ObservableCollection<int>(Enumerable.Range(-12, 27));

        /// <summary>
        /// Collection of computation frequencies (in minutes).
        /// </summary>
        public ObservableCollection<double> CompFrequencies { get; } = new ObservableCollection<double> { 1, 5, 15, 60 };

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationViewModel"/> class.
        /// </summary>
        public ConfigurationViewModel(Configuration config) => this.config = config;

        /// <summary> 
        /// Geographic latitude for calculations. 
        /// </summary>
        public double Latitude
        {
            get => config.Latitude;
            set {  config.Latitude = value; OnPropertyChanged(); }
        }

        /// <summary> 
        /// Geographic longitude for calculations. 
        /// </summary>
        public double Longitude
        {
            get => config.Longitude;
            set { config.Longitude = value; OnPropertyChanged(); }
        }

        /// <summary> 
        /// Time zone offset from UTC.
        /// </summary>
        public int TimeZone
        {
            get => config.TimeZone;
            set { config.TimeZone = value; OnPropertyChanged(); }
        }

        /// <summary> 
        /// Method used for solar coordinate calculations. 
        /// </summary>
        public SunCoorCalcMethod CalcMethod
        {
            get => config.CalcMethod;
            set { config.CalcMethod = value; OnPropertyChanged(); }
        }

        /// <summary> 
        /// GOST standard offset. 
        /// </summary>
        public double GOSToffset
        {
            get => config.GOSToffset;
            set { config.GOSToffset = value; OnPropertyChanged(); }
        }

        /// <summary> 
        /// The day for which insolation calculations are performed. 
        /// </summary>
        public DateTime CalcDay
        {
            get => config.CalcDay;
            set { config.CalcDay = value; OnPropertyChanged(); }
        }

        /// <summary> 
        /// Frequency of computations (in minutes). 
        /// </summary>
        public double CompFreq
        {
            get => config.CompFreq;
            set { config.CompFreq = value; OnPropertyChanged(); }
        }

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <c>PropertyChanged</c> event to notify the UI of property updates.
        /// </summary>
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}

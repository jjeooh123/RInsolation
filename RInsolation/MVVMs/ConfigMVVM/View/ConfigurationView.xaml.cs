using System.Windows;

namespace Insolation.MVVMs
{
    /// <summary>
    /// Interaction logic for <see cref="ConfigurationView"/>.
    /// Acts as a WPF window for editing application configuration.
    /// </summary>
    public partial class ConfigurationView : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationView"/> class,
        /// binding it to the provided <see cref="ConfigurationViewModel"/>.
        /// </summary>
        public ConfigurationView(ConfigurationViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}

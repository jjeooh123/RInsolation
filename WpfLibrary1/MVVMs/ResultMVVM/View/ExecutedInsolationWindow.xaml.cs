using System.Windows;

namespace Insolation.MVVMs
{
    /// <summary>
    /// WPF window for displaying executed insolation results.
    /// Provides a data-bound view over <see cref="MainInsolationViewModel"/>.
    /// </summary>
    public partial class ExecutedInsolationWindowView : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutedInsolationWindowView"/> class
        /// with a provided <see cref="MainInsolationViewModel"/> as its DataContext.
        /// </summary>
        /// <param name="viewModel">ViewModel supplying data for the window.</param>
        public ExecutedInsolationWindowView(MainInsolationViewModel viewModel) // TODO: consider DI/global context
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Insolation.Config;
using Insolation.InsolationCalculator;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Insolation.MVVMs
{
    /// <summary>
    /// Main ViewModel for managing and displaying insolation results.
    /// Provides data binding for a collection of <see cref="InsolationViewModel"/> instances,
    /// integrates with Revit's <see cref="Autodesk.Revit.UI.UIDocument"/> to allow element selection in the Revit model,
    /// and supports row-element synchronization.
    /// </summary>
    public class MainInsolationViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Collection of calculated insolation results to be displayed in the UI.
        /// </summary>
        public ObservableCollection<InsolationViewModel> InsolationPoints { get; }

        private UIDocument uIDoc;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainInsolationViewModel"/> class.
        /// </summary>
        /// <param name="points">Collection of executed insolation points containing calculation results.</param>
        /// <param name="config">Configuration settings used for evaluation.</param>
        /// <param name="uiDoc">Reference to the active Revit <see cref="Autodesk.Revit.UI.UIDocument"/> for element selection.</param>
        public MainInsolationViewModel(IEnumerable<ExecutedInsolationPoint> points, Configuration config, UIDocument uiDoc)
        {
            // Convert each ExecutedInsolationPoint into a ViewModel for data binding
            InsolationPoints = new ObservableCollection<InsolationViewModel>(
                points.Select(p => new InsolationViewModel(p, config))
            );

            this.uIDoc = uiDoc;

            // Setup command for selecting Revit elements from UI
            SelectElementCommand = new RelayCommand(SelectElement, CanSelectElement);
        }

        #region Select element in view, based on selected element in revit

        private InsolationViewModel selectedInsolationPoint;

        /// <summary>
        /// Currently selected insolation point in the UI
        /// </summary>
        public InsolationViewModel SelectedInsolationPoint
        {
            get => selectedInsolationPoint;
            set
            {
                if (selectedInsolationPoint != value)
                {
                    selectedInsolationPoint = value;
                    OnPropertyChanged(nameof(SelectedInsolationPoint));
                }
            }
        }

        /// <summary>
        /// select a row in the UI based on a Revit Selected <see cref="Autodesk.Revit.DB.Element"/>.
        /// Used to synchronize selection between Revit model and UI grid.
        /// </summary>
        public void SelectRowByElementId(ElementId elementId)
        {
            var match = InsolationPoints.FirstOrDefault(p => p.ElementId == elementId.Value);
            if (match != null)
                SelectedInsolationPoint = match;
        }

        #endregion

        #region select element in revit

        /// <summary>
        /// Command bound to UI actions (e.g. button click) for selecting Revit elements.
        /// </summary>
        public ICommand SelectElementCommand { get; }

        /// <summary>
        /// Determines if a Revit element can be selected (parameter must not be null).
        /// </summary>
        private bool CanSelectElement(object para) => para != null;

        /// <summary>
        /// Selects the corresponding Revit element in the active document based on the chosen <see cref="InsolationViewModel"/>.
        /// </summary>
        private void SelectElement(object para)
            => uIDoc.Selection.SetElementIds(new[] { new ElementId((para as InsolationViewModel).ElementId) });

        #endregion

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <c>PropertyChanged</c> event to notify UI of property updates.
        /// </summary>
        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

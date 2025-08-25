using Insolation.InsolationCalculator;
using Insolation.Helpers;
using Insolation.Config;

namespace Insolation.MVVMs
{
    /// <summary>
    /// ViewModel representing calculated insolation results for a given element.
    /// </summary>
    public class InsolationViewModel
    {
        private readonly ExecutedInsolationPoint executedInsolationPoint;
        private readonly Configuration config;

        /// <summary> 
        /// Identifier of the associated element. 
        /// </summary>
        public long ElementId { get; set; }

        /// <summary> 
        /// Duration of continuous insolation for the associated element.  
        /// This is not the total daily insolation time, but the length of the longest continuous interval with a single gap under permissible threshold.
        /// </summary>
        public TimeSpan InsolationTime { get; set; }

        /// <summary> 
        /// Continuous insolation time intervals expressed as formatted time intervals. 
        /// </summary>
        public string InsolationTimeInInteravlas { get; set; }

        /// <summary> 
        /// Full list of insolation intervals as a formatted string.
        /// </summary>
        public string Intervals { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsolationViewModel"/> class
        /// and evaluates insolation metrics based on executed calculation data.
        /// </summary>
        public InsolationViewModel(ExecutedInsolationPoint executedInsolationPoint, Configuration config)
        {
            this.executedInsolationPoint = executedInsolationPoint;
            this.config = config;

            this.ElementId = executedInsolationPoint.InsolationPoint.ElementId.Value;

            // Evaluate continuous insolation time and it's intervals
            EvaluteInsolationTime(config, out var InsolationTime, out var InsolationTimeInInteravlas);
            
            this.InsolationTime = InsolationTime;
            this.InsolationTimeInInteravlas = TimeIntervalsHelper.TimeIntervalsToString(InsolationTimeInInteravlas);
            this.Intervals = TimeIntervalsHelper.TimeIntervalsToString(executedInsolationPoint.InsolationIntervals);
        }

        /// <summary>
        /// Evaluates continuous insolation time.
        /// </summary>
        /// <param name="config">Configuration settings containing permissible gap.</param>
        /// <param name="insolationTime">Resulting total insolation time.</param>
        /// <param name="InsolationTimeInInteravlas">Resulting set of insolation intervals.</param>
        private void EvaluteInsolationTime(Configuration config,
                                           out TimeSpan insolationTime,
                                           out IEnumerable<DateTime> InsolationTimeInInteravlas)
        {
            var intervals = executedInsolationPoint.InsolationIntervals;

            // Case: no intervals -> no insolation
            if (intervals.Count() == 0)
            {
                insolationTime = TimeSpan.Zero;
                InsolationTimeInInteravlas = new List<DateTime>();
                return;
            }

            // Case: two points -> simple interval
            else if (intervals.Count() == 2)
            {
                insolationTime = intervals.ElementAt(1) - intervals.ElementAt(0);
                InsolationTimeInInteravlas = intervals;
                return;
            }

            // Case: multiple intervals -> merge if gap <= permissible threshold
            TimeSpan permissibleGap = new TimeSpan(0, config.PermissibleGap, 0);
            Dictionary<TimeSpan, IEnumerable<DateTime>> TimeSpanDictionary = new Dictionary<TimeSpan, IEnumerable<DateTime>>();

            // Initialize with first interval
            TimeSpanDictionary[intervals.ElementAt(1) - intervals.ElementAt(0)] = new List<DateTime>() { intervals.ElementAt(0), intervals.ElementAt(1) };

            // Merge intervals if the gap between them is small enough
            for (int i = 0; i < intervals.Count() - 4; i += 2)
                if (intervals.ElementAt(i + 2) - intervals.ElementAt(i + 1) <= permissibleGap)
                    TimeSpanDictionary[intervals.ElementAt(i + 1) - intervals.ElementAt(i) + (intervals.ElementAt(i + 3) - intervals.ElementAt(i + 2))] =
                        new List<DateTime>() { intervals.ElementAt(i), intervals.ElementAt(i + 1), intervals.ElementAt(i + 2), intervals.ElementAt(i + 3) };
                else TimeSpanDictionary[intervals.ElementAt(i + 3) - intervals.ElementAt(i + 2)] =
                        new List<DateTime>() { intervals.ElementAt(i + 2), intervals.ElementAt(i + 3) };

            // Select the longest insolation duration from merged intervals
            insolationTime = TimeSpanDictionary.Keys.Max();
            InsolationTimeInInteravlas = TimeSpanDictionary[TimeSpanDictionary.Keys.Max()];
        }
    }
}

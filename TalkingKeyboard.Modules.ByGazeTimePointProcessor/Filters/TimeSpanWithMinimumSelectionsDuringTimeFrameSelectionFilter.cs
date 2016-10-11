// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimeSpanWithMinimumSelectionsDuringTimeFrameSelectionFilter.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the TimeSpanWithMinimumSelectionsDuringTimeFrameSelectionFilter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.ByGazeTimePointProcessor.Filters
{
    using System;
    using System.Linq;
    using System.Windows;

    using TalkingKeyboard.Infrastructure;
    using TalkingKeyboard.Infrastructure.DataContainers;

    /// <summary>
    ///     This class implements a sample <see cref="SelectionFilter{T}" /> which activates a key if (at least) a given number
    ///     of points have recently fallen on said key. The maximum considered time is established as a <see cref="TimeSpan" />
    /// </summary>
    public class TimeSpanWithMinimumSelectionsDuringTimeFrameSelectionFilter : SelectionFilter<TimedControlsWithPoint>
    {
        private readonly int pointsRequired;
        private readonly TimeSpan timeFrame;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TimeSpanWithMinimumSelectionsDuringTimeFrameSelectionFilter" /> class.
        /// </summary>
        /// <param name="window">The window on which the selectable controls are located.</param>
        /// <param name="timeFrame">The time frame from current time during which points must have fallen to be considered.</param>
        /// <param name="pointsRequired">The number of points required for activation.</param>
        public TimeSpanWithMinimumSelectionsDuringTimeFrameSelectionFilter(
            Window window,
            TimeSpan timeFrame,
            int pointsRequired)
            : base(window)
        {
            this.timeFrame = timeFrame;
            this.pointsRequired = pointsRequired;
        }

        /// <summary>
        ///     Filters the collection, selects accordingly and returns an updated collection.
        /// </summary>
        /// <param name="c">The point collection.</param>
        /// <returns>The updated point collection.</returns>
        public override TimedControlsWithPoint SelectAndUpdate(TimedControlsWithPoint c)
        {
            var result = new TimedControlsWithPoint(Configuration.PointKeepAliveTimeSpan, this.Window);
            foreach (var ctp in c)
            {
                var timedPoints = ctp.Value;
                if (timedPoints == null)
                {
                    continue;
                }

                var pointCount = timedPoints.Count;
                if ((pointCount >= this.pointsRequired)
                    && (timedPoints.Keys.Last() - timedPoints.Keys.First() >= this.timeFrame))
                {
                    this.Window.Dispatcher.Invoke(() => ctp.Key.Select());
                }
                else
                {
                    result.TryAdd(ctp.Key, ctp.Value);
                }
            }

            return result;
        }
    }
}
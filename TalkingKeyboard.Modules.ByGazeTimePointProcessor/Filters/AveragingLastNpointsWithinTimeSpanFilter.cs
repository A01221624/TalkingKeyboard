// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AveragingLastNpointsWithinTimeSpanFilter.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the AveragingLastNpointsWithinTimeSpanFilter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.ByGazeTimePointProcessor.Filters
{
    using System;
    using System.Linq;
    using System.Windows;

    using TalkingKeyboard.Infrastructure.DataContainers;

    public class AveragingLastNpointsWithinTimeSpanFilter : AveragingFilter
    {
        private readonly int _numberOfPoints;
        private readonly TimeSpan _timeSpan;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AveragingLastNpointsWithinTimeSpanFilter" /> class.
        /// </summary>
        /// <param name="numberOfPoints">The number of points.</param>
        /// <param name="timeSpan">The time span.</param>
        public AveragingLastNpointsWithinTimeSpanFilter(int numberOfPoints, TimeSpan timeSpan)
        {
            this._numberOfPoints = numberOfPoints;
            this._timeSpan = timeSpan;
        }

        /// <summary>
        ///     Filters the specified timed points.
        /// </summary>
        /// <param name="timedPoints">The timed points.</param>
        /// <returns>
        ///     An average of the last N points (as long as they fall within a given timespan from <see cref="DateTime.Now" />).
        /// </returns>
        public override Point? Filter(TimedPoints timedPoints)
        {
            if (timedPoints == null)
            {
                return null;
            }

            var withinTimeSpan = timedPoints.Where(pair => DateTime.Now - pair.Key <= this._timeSpan).ToList();
            if (withinTimeSpan.Count() < this._numberOfPoints)
            {
                return null;
            }

            var pointsToAverage = withinTimeSpan.OrderByDescending(pair => pair.Key).Take(this._numberOfPoints).ToList();
            var averageX = pointsToAverage.Select(pair => pair.Value.X).Average();
            var averageY = pointsToAverage.Select(pair => pair.Value.Y).Average();

            return new Point(averageX, averageY);
        }
    }
}
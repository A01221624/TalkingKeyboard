// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AveragingFilter.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the AveragingFilter abstract type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.ByGazeTimePointProcessor.Filters
{
    using System.Windows;

    using TalkingKeyboard.Infrastructure.DataContainers;

    /// <summary>
    ///     This abstract class provides a base for other classes to act as filters which take a number of coordinate points
    ///     and return an average.
    /// </summary>
    public abstract class AveragingFilter
    {
        /// <summary>
        ///     Filters the specified timed points.
        /// </summary>
        /// <param name="timedPoints">The timed points.</param>
        /// <returns>
        ///     An average of the points. The actual average depends on implementation (e.g. it could be an average of the
        ///     last 5 points, or of the 10 points nearest to the last in a given timespan)
        /// </returns>
        public abstract Point? Filter(TimedPoints timedPoints);
    }
}
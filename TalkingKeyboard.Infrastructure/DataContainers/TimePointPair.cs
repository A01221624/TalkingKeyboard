// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimePointPair.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the TimePointPair type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TalkingKeyboard.Infrastructure.DataContainers
{
    using System;
    using System.Windows;

    public class TimePointPair
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimePointPair"/> class.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="point">The point.</param>
        public TimePointPair(DateTime time, Point point)
        {
            this.Point = point;
            this.Time = time;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimePointPair"/> class.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <remarks><see cref="Time"/>Is set to <see cref="DateTime.Now"/></remarks>
        public TimePointPair(Point point)
        {
            this.Point = point;
            this.Time = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the point.
        /// </summary>
        /// <value>
        /// The point.
        /// </value>
        public Point Point { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public DateTime Time { get; set; }
    }
}
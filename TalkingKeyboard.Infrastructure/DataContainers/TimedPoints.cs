// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimedPoints.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the TimedPoints type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.DataContainers
{
    using System;
    using System.Linq;
    using System.Windows;

    public class TimedPoints : MaintainablePointCollection<DateTime, Point>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TimedPoints" /> class.
        /// </summary>
        /// <param name="pointKeepAliveTimeSpan">The time for which the point will be remembered.</param>
        public TimedPoints(TimeSpan pointKeepAliveTimeSpan)
            : base(pointKeepAliveTimeSpan)
        {
        }

        /// <summary>
        ///     Adds a point.
        /// </summary>
        /// <param name="point">The point.</param>
        public override void AddPoint(Point point)
        {
            var currentTime = DateTime.Now;
            this.AddOrUpdate(currentTime, point, (_, __) => point);
        }

        /// <summary>
        ///     Removes any points older than
        ///     <see cref="F:TalkingKeyboard.Infrastructure.DataContainers.MaintainablePointCollection`2.PointKeepAliveTimeSpan" />
        ///     from this.
        /// </summary>
        public override void Maintain()
        {
            if (DateTime.Now - this.LastMaintainedTime < this.PointKeepAliveTimeSpan)
            {
                return;
            }

            var oldestAcceptable = DateTime.Now - this.PointKeepAliveTimeSpan;
            var acceptableValues = this.Where(pair => pair.Key <= oldestAcceptable).ToList();
            this.Clear();
            foreach (var e in acceptableValues)
            {
                this.TryAdd(e.Key, e.Value);
            }

            this.LastMaintainedTime = DateTime.Now;
        }
    }
}
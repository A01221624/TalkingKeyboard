// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimedControlsWithPoint.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the TimedControlsWithPoint type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.DataContainers
{
    using System;
    using System.Windows;

    using TalkingKeyboard.Infrastructure.Controls;
    using TalkingKeyboard.Infrastructure.Helpers;

    public class TimedControlsWithPoint : MaintainablePointCollection<SelectableControl, TimedPoints>
    {
        private readonly Window _window;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TimedControlsWithPoint" /> class.
        /// </summary>
        /// <param name="pointKeepAliveTimeSpan">The time for which the point will be remembered.</param>
        /// <param name="window">The window where the controls are located.</param>
        public TimedControlsWithPoint(TimeSpan pointKeepAliveTimeSpan, Window window)
            : base(pointKeepAliveTimeSpan)
        {
            this._window = window;
        }

        /// <summary>
        ///     Registers a new point to the control on which it falls, if it is located on any control.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <remarks>Does nothing if the point does not fall on any control</remarks>
        public override void AddPoint(Point point)
        {
            var seenControl = HitTestHelper.SelectableControlUnderPoint(point, this._window);
            if (seenControl == null)
            {
                return;
            }

            if (!this.ContainsKey(seenControl))
            {
                this.TryAdd(seenControl, new TimedPoints(this.PointKeepAliveTimeSpan));
            }

            this[seenControl].AddPoint(point);
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

            foreach (var tp in this.Values)
            {
                tp.Maintain();
            }

            this.LastMaintainedTime = DateTime.Now;
        }
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GazeTimePerControl.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the GazeTimePerControl type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.ByGazeTimePointProcessor.DataContainers
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    using TalkingKeyboard.Infrastructure.Controls;
    using TalkingKeyboard.Infrastructure.DataContainers;
    using TalkingKeyboard.Infrastructure.Helpers;

    internal class GazeTimePerControl : MaintainablePointCollection<SelectableControl, TimeSpan>
    {
        private readonly TimeSpan _gazeKeepAliveTimeSpan;
        private readonly Window _window;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GazeTimePerControl" /> class.
        /// </summary>
        /// <param name="gazeKeepAliveTimeSpan">The gaze keep alive time span.</param>
        /// <param name="window">The window containing the controls.</param>
        public GazeTimePerControl(TimeSpan gazeKeepAliveTimeSpan, Window window)
        {
            this._gazeKeepAliveTimeSpan = gazeKeepAliveTimeSpan;
            this._window = window;
            this.LastTimePerControl = new Dictionary<SelectableControl, DateTime>();
        }

        /// <summary>
        ///     Gets the last time each control was seen.
        /// </summary>
        /// <value>
        ///     Dictionary mapping controls (keys) to last time seen (values).
        /// </value>
        private IDictionary<SelectableControl, DateTime> LastTimePerControl { get; }

        /// <summary>
        ///     Registers a new point if it fell over a control and the control it fell on.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <remarks>
        ///     Adds a new control to the dictionary if necessary, then updates the last time the control was seen.
        /// </remarks>
        public override void AddPoint(Point point)
        {
            var seenControl = HitTestHelper.SelectableControlUnderPoint(point, this._window);
            if (seenControl == null)
            {
                return;
            }

            var oldestAcceptable = DateTime.Now - this._gazeKeepAliveTimeSpan;
            DateTime lastTimeSeen;
            if (!this.LastTimePerControl.TryGetValue(seenControl, out lastTimeSeen))
            {
                lastTimeSeen = DateTime.MinValue;
                this.LastTimePerControl.Add(seenControl, DateTime.Now);
            }

            this.AddOrUpdate(
                seenControl,
                TimeSpan.Zero,
                (control, oldTimeSpan) =>
                        oldestAcceptable > lastTimeSeen ? TimeSpan.Zero : oldTimeSpan + (DateTime.Now - lastTimeSeen));
            this.LastTimePerControl[seenControl] = DateTime.Now;
        }

        /// <summary>
        ///     Removes any points older than <see cref="_gazeKeepAliveTimeSpan" />
        ///     from this.
        /// </summary>
        public override void Maintain()
        {
            var oldestAcceptable = DateTime.Now - this._gazeKeepAliveTimeSpan;
            foreach (var e in this)
            {
                this.AddOrUpdate(
                    e.Key,
                    TimeSpan.Zero,
                    (control, oldTimeSpan) =>
                            oldestAcceptable > this.LastTimePerControl[control] ? TimeSpan.Zero : oldTimeSpan);
            }
        }
    }
}
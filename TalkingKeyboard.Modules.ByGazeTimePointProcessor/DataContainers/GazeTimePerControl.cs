using System;
using System.Collections.Generic;
using System.Windows;
using TalkingKeyboard.Infrastructure.Controls;
using TalkingKeyboard.Infrastructure.DataContainers;
using TalkingKeyboard.Infrastructure.Helpers;

namespace TalkingKeyboard.Modules.ByGazeTimePointProcessor.DataContainers
{
    internal class GazeTimePerControl : MaintainablePointCollection<SelectableControl, TimeSpan>
    {
        private readonly TimeSpan _gazeKeepAliveTimeSpan;
        private readonly Window _window;

        public GazeTimePerControl(TimeSpan gazeKeepAliveTimeSpan, Window window)
        {
            _gazeKeepAliveTimeSpan = gazeKeepAliveTimeSpan;
            _window = window;
            LastTimePerControl = new Dictionary<SelectableControl, DateTime>();
        }

        private IDictionary<SelectableControl, DateTime> LastTimePerControl { get; }

        public override void Maintain()
        {
            var oldestAcceptable = DateTime.Now - _gazeKeepAliveTimeSpan;
            foreach (var e in this)
            {
                AddOrUpdate(e.Key, TimeSpan.Zero,
                    (control, oldTimeSpan) =>
                        oldestAcceptable > LastTimePerControl[control] ? TimeSpan.Zero : oldTimeSpan);
            }
        }

        public override void AddPoint(Point point)
        {
            var seenControl = HitTestHelper.SelectableControlUnderPoint(point, _window);
            if (seenControl == null) return;
            var oldestAcceptable = DateTime.Now - _gazeKeepAliveTimeSpan;
            DateTime lastTimeSeen;
            if (!LastTimePerControl.TryGetValue(seenControl, out lastTimeSeen))
            {
                lastTimeSeen = DateTime.MinValue;
                LastTimePerControl.Add(seenControl, DateTime.Now);
            }
            AddOrUpdate(seenControl, TimeSpan.Zero,
                (control, oldTimeSpan) =>
                    oldestAcceptable > lastTimeSeen ? TimeSpan.Zero : oldTimeSpan + (DateTime.Now - lastTimeSeen));
            LastTimePerControl[seenControl] = DateTime.Now;
        }
    }
}
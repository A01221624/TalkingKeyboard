using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TalkingKeyboard.Infrastructure.Controls;
using TalkingKeyboard.Infrastructure.DataContainers;
using TalkingKeyboard.Shell.Helpers;

namespace TalkingKeyboard.Shell.DataContainers
{
    public class TimedControlsWithPoint : MaintainablePointCollection<SelectableControl, TimedPoints>
    {
        private readonly Window _window;

        public TimedControlsWithPoint(TimeSpan pointKeepAliveTimeSpan, Window window) : base(pointKeepAliveTimeSpan)
        {
            _window = window;
        }

        public override void Maintain()
        {
            if (DateTime.Now - LastMaintainedTime < PointKeepAliveTimeSpan) return;
            foreach (var tp in Collection.Values)
            {
                tp.Maintain();
            }
            LastMaintainedTime = DateTime.Now;
        }

        public override void Add(Point point)
        {
            _window.Dispatcher.Invoke(() =>
            {
                var seenControl = HitTestHelper.SelectableControlUnderPoint(point, _window);
                if (seenControl == null) return;
                if (!Collection.ContainsKey(seenControl))
                {
                    Collection.Add(seenControl, new TimedPoints(PointKeepAliveTimeSpan));
                }
                Collection[seenControl].Add(point);
            });
        }
    }
}
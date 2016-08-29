using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TalkingKeyboard.Infrastructure.DataContainers
{
    public class TimedPoints : MaintainablePointCollection<DateTime, Point>
    {
        public TimedPoints(TimeSpan pointKeepAliveTimeSpan) : base(pointKeepAliveTimeSpan)
        {
        }

        public override void Maintain()
        {
            if (DateTime.Now - LastMaintainedTime < PointKeepAliveTimeSpan) return;
            var oldestAcceptable = DateTime.Now - PointKeepAliveTimeSpan;
            Collection =
                new SortedList<DateTime, Point>(Collection.SkipWhile(e => e.Key > oldestAcceptable)
                    .ToDictionary(e => e.Key, e => e.Value));
            LastMaintainedTime = DateTime.Now;
        }

        public override void Add(Point point)
        {
            Collection.Add(DateTime.Now, point);
        }
    }
}
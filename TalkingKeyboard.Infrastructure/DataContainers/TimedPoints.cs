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
            var acceptableValues = this.Where(pair => pair.Key <= oldestAcceptable).ToList();
            Clear();
            foreach (var e in acceptableValues)
            {
                TryAdd(e.Key, e.Value);
            }
            LastMaintainedTime = DateTime.Now;
        }

        public override void AddPoint(Point point)
        {
            var currentTime = DateTime.Now;
            AddOrUpdate(currentTime, point, (_, __) => point);
        }
    }
}
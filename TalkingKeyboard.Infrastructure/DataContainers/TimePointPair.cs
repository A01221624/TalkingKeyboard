using System;
using System.Windows;

namespace TalkingKeyboard.Infrastructure.DataContainers
{
    public class TimePointPair
    {
        public TimePointPair(DateTime time, Point point)
        {
            Point = point;
            Time = time;
        }

        public TimePointPair(Point point)
        {
            Point = point;
            Time = DateTime.Now;
        }

        public DateTime Time { get; set; }
        public Point Point { get; set; }
    }
}
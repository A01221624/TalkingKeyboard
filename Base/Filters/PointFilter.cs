using System.Windows;
using TimedPoint = System.Tuple<System.DateTime, System.Windows.Point>;

namespace TalkingKeyboard.Shell.Filters
{
    public abstract class PointFilter
    {
        protected Window Window { get; set; }

        protected PointFilter(Window window)
        {
            Window = window;
        }

        public abstract FixedSizeQueue<TimedPoint> Filter(FixedSizeQueue<TimedPoint> pointBuffer);
    }
}
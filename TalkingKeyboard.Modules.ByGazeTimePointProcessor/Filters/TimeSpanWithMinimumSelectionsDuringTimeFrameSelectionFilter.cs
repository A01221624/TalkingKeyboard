using System;
using System.Linq;
using System.Windows;
using TalkingKeyboard.Infrastructure;
using TalkingKeyboard.Infrastructure.DataContainers;
using TimedPoint = System.Tuple<System.DateTime, System.Windows.Point>;

namespace TalkingKeyboard.Modules.ByGazeTimePointProcessor.Filters
{
    public class TimeSpanWithMinimumSelectionsDuringTimeFrameSelectionFilter : SelectionFilter<TimedControlsWithPoint>
    {
        private readonly int _pointsRequired;
        private readonly TimeSpan _timeFrame;

        public TimeSpanWithMinimumSelectionsDuringTimeFrameSelectionFilter(Window window, TimeSpan timeFrame,
            int pointsRequired) : base(window)
        {
            _timeFrame = timeFrame;
            _pointsRequired = pointsRequired;
        }

        /*
    Algorithm:
    1.- Traverse the Queue by Dequeuing
    1.1.- AddPoint each element to its corresponding element's Queue (or discard if too old or no element)
    2.- Any Queues with more or equal than N elements where the TimeSpan is reached call it's elment's Select().
    3.- Any other Queues are combined and sent back.
    */

        public override TimedControlsWithPoint SelectAndUpdate(TimedControlsWithPoint c)
        {
            var result = new TimedControlsWithPoint(Configuration.PointKeepAliveTimeSpan, Window);
            foreach (var ctp in c)
            {
                var timedPoints = ctp.Value;
                if (timedPoints == null) continue;
                var pointCount = timedPoints.Count;
                if (pointCount >= _pointsRequired
                    && timedPoints.Keys.Last() - timedPoints.Keys.First() >= _timeFrame)
                {
                    Window.Dispatcher.Invoke(() => ctp.Key.Select());
                }
                else
                {
                    result.TryAdd(ctp.Key, ctp.Value);
                }
            }
            return result;
        }
    }
}
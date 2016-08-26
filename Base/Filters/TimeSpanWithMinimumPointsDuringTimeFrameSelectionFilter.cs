using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TalkingKeyboard.Infrastructure.Controls;
using TalkingKeyboard.Shell.Helpers;
using TimedPoint = System.Tuple<System.DateTime, System.Windows.Point>;

namespace TalkingKeyboard.Shell.Filters
{
    public class TimeSpanWithMinimumPointsDuringTimeFrameSelectionFilter : PointFilter
    {
        private readonly TimeSpan _timeFrame;
        private readonly int _pointsRequired;

        public TimeSpanWithMinimumPointsDuringTimeFrameSelectionFilter(Window window, TimeSpan timeFrame, int pointsRequired) : base(window)
        {
            _timeFrame = timeFrame;
            _pointsRequired = pointsRequired;
        }

        /*
    Algorithm:
    1.- Traverse the Queue by Dequeuing
    1.1.- Add each element to its corresponding element's Queue (or discard if too old or no element)
    2.- Any Queues with more or equal than N elements where the TimeSpan is reached call it's elment's Select().
    3.- Any other Queues are combined and sent back.
    */

        public override FixedSizeQueue<TimedPoint> Filter(FixedSizeQueue<TimedPoint> pointBuffer)
        {
            var remainingPoints = new FixedSizeQueue<TimedPoint>(pointBuffer.Size);
            Window.Dispatcher.Invoke(() =>
            {
                TimedPoint tp = null;
                var foundSelectableControls = new Dictionary<SelectableControl, Queue<TimedPoint>>();
                while (pointBuffer.TryDequeue(out tp))
                {
                    var point = tp.Item2;
                    var pointTime = tp.Item1;
                    if (DateTime.Now - pointTime > _timeFrame + TimeSpan.FromMilliseconds(10)) continue;
                    var seenControl = HitTestHelper.SelectableControlUnderPoint(point, Window);
                    if (seenControl == null) continue;
                    if (!foundSelectableControls.ContainsKey(seenControl))
                    {
                        foundSelectableControls.Add(seenControl, new Queue<TimedPoint>());
                    }
                    foundSelectableControls[seenControl].Enqueue(tp);
                }
                foreach (var controlKeyVal in foundSelectableControls)
                {
                    if (controlKeyVal.Value.Count >= _pointsRequired &&
                    controlKeyVal.Value.Last().Item1 - controlKeyVal.Value.First().Item1 >= _timeFrame)
                    {
                        controlKeyVal.Key.Select();
                    }
                    else
                    {
                        foreach (var point in controlKeyVal.Value)
                        {
                            remainingPoints.Enqueue(point);
                        }
                    }
                }
            });
            return remainingPoints;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Windows;
using TalkingKeyboard.Infrastructure.Controls;
using TalkingKeyboard.Shell.Helpers;
using TimedPoint = System.Tuple<System.DateTime, System.Windows.Point>;

namespace TalkingKeyboard.Shell.Filters
{
    //public class AmountOfSelectionsDuringTimeFrameSelectionFilter : SelectionFilter<,>
    //{
    //    private readonly TimeSpan _timeFrame;
    //    private readonly int _pointsRequired;

    //    public AmountOfSelectionsDuringTimeFrameSelectionFilter(Window window, TimeSpan timeFrame, int pointsRequired) : base(window)
    //    {
    //        _timeFrame = timeFrame;
    //        _pointsRequired = pointsRequired;
    //    }

    //    /*
    //    Algorithm:
    //    1.- Traverse the Queue by Dequeuing
    //    1.1.- AddPoint each element to its corresponding element's Queue (or discard if too old or no element)
    //    2.- Any Queues with more or equal than N elements call it's elment's Select().
    //    3.- Any Queues with less than N elements are combined and sent back.

    //    */
    //    public override FixedSizeQueue<TimedPoint> Filter(FixedSizeQueue<TimedPoint> pointBuffer)
    //    {
    //        var remainingPoints = new FixedSizeQueue<TimedPoint>(pointBuffer.Size);
    //        Window.Dispatcher.Invoke(() =>
    //        {
    //            TimedPoint tp = null;
    //        var foundSelectableControls = new Dictionary<SelectableControl, Queue<TimedPoint>>();
    //        while (pointBuffer.TryDequeue(out tp))
    //        {
    //            var point = tp.Item2;
    //            var pointTime = tp.Item1;
    //            if (DateTime.Now - pointTime > _timeFrame) continue;
    //            var seenControl = HitTestHelper.SelectableControlUnderPoint(point, Window);
    //            if (seenControl == null) continue;
    //            if (!foundSelectableControls.ContainsKey(seenControl))
    //            {
    //                foundSelectableControls.Add(seenControl, new Queue<TimedPoint>());
    //            }
    //            foundSelectableControls[seenControl].Enqueue(tp);
    //        }
    //        foreach (var controlKeyVal in foundSelectableControls)
    //        {
    //            if (controlKeyVal.Value.Count >= _pointsRequired)
    //            {
    //                controlKeyVal.Key.Select();
    //            }
    //            else
    //            {
    //                foreach (var point in controlKeyVal.Value)
    //                {
    //                    remainingPoints.Enqueue(point);
    //                }
    //            }
    //        }
    //        });
    //        return remainingPoints;
    //    }
    //}
}
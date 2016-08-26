using System;
using TalkingKeyboard.Shell.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using TalkingKeyboard.Infrastructure.Controls;
using TimedPoint = System.Tuple<System.DateTime, System.Windows.Point>;

namespace TalkingKeyboard.Shell.Filters
{
    public class TwoPointsDuringTimeFrameSelectionFilter : PointFilter
    {
        private readonly int _gazeTimeMilliseconds;
        private readonly int _cooldownMilliseconds;

        public TwoPointsDuringTimeFrameSelectionFilter(Window window, int gazeTimeMilliseconds, int cooldownMilliseconds) : base(window)
        {
            _gazeTimeMilliseconds = gazeTimeMilliseconds;
            _cooldownMilliseconds = cooldownMilliseconds;
        }

        public override FixedSizeQueue<TimedPoint> Filter(FixedSizeQueue<TimedPoint> pointCollection)
        {
            Dictionary<SelectableControl, DateTime> knownSeenControls = new Dictionary<SelectableControl, DateTime>();
            Window.Dispatcher.Invoke(() =>
            {
                foreach (var timedPoint in pointCollection)
                {
                    var seenControl = HitTestHelper.SelectableControlUnderPoint(timedPoint.Item2, Window);
                    if (seenControl == null) continue;
                    var newTimestamp = timedPoint.Item1;
                    if (!knownSeenControls.ContainsKey(seenControl))
                    {
                        knownSeenControls.Add(seenControl, newTimestamp);
                    }
                    else
                    {
                        var oldestTimestamp = knownSeenControls[seenControl];
                        if (newTimestamp - oldestTimestamp < TimeSpan.FromMilliseconds(_gazeTimeMilliseconds)) continue;
                        if (DateTime.Now - seenControl?.LastSelectedTime >= TimeSpan.FromMilliseconds(_cooldownMilliseconds))
                            seenControl?.Select();
                    }
                }
            });
            return pointCollection;
        }
    }
}
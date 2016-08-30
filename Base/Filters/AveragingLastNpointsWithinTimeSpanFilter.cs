using System;
using System.Linq;
using System.Windows;
using TalkingKeyboard.Infrastructure.DataContainers;

namespace TalkingKeyboard.Shell.Filters
{
    public class AveragingLastNpointsWithinTimeSpanFilter : AveragingFilter
    {
        private readonly int _numberOfPoints;
        private readonly TimeSpan _timeSpan;

        public AveragingLastNpointsWithinTimeSpanFilter(int numberOfPoints, TimeSpan timeSpan)
        {
            _numberOfPoints = numberOfPoints;
            _timeSpan = timeSpan;
        }

        public override Point? Filter(TimedPoints timedPoints)
        {
            if (timedPoints == null) return null;
            var withinTimeSpan = timedPoints.Where(pair => DateTime.Now - pair.Key <= _timeSpan).ToList();
            if (withinTimeSpan.Count() < _numberOfPoints) return null;
            var pointsToAverage = withinTimeSpan.OrderByDescending(pair => pair.Key).Take(_numberOfPoints).ToList();
            var averageX = pointsToAverage.Select(pair => pair.Value.X).Average();
            var averageY = pointsToAverage.Select(pair => pair.Value.Y).Average();

            return new Point(averageX, averageY);
        }
    }
}
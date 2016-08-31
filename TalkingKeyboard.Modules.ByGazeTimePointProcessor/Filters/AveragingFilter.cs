using System.Windows;
using TalkingKeyboard.Infrastructure.DataContainers;

namespace TalkingKeyboard.Modules.ByGazeTimePointProcessor.Filters
{
    public abstract class AveragingFilter
    {
        public abstract Point? Filter(TimedPoints timedPoints);
    }
}
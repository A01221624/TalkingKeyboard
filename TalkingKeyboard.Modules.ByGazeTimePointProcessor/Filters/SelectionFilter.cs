using System.Windows;
using TimedPoint = System.Tuple<System.DateTime, System.Windows.Point>;

namespace TalkingKeyboard.Modules.ByGazeTimePointProcessor.Filters
{
    public abstract class SelectionFilter<T>
    {
        protected Window Window { get; set; }

        protected SelectionFilter(Window window)
        {
            Window = window;
        }

        public abstract T SelectAndUpdate(T pointCollection);
    }
}
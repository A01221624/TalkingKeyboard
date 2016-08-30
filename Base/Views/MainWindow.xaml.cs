using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Prism.Events;
using TalkingKeyboard.Infrastructure;
using TalkingKeyboard.Infrastructure.Controls;
using TalkingKeyboard.Infrastructure.DataContainers;
using TalkingKeyboard.Shell.DataContainers;
using TalkingKeyboard.Shell.Filters;
using TimedPoint = System.Tuple<System.DateTime, System.Windows.Point>;

namespace TalkingKeyboard.Shell.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly IEventAggregator _eventAggregator;
        private SelectionFilter<TimedControlsWithPoint> _selectionFilter;
        private AveragingFilter _averagingFilter;

        public MainWindow(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            _eventAggregator = eventAggregator;

            RecentGazePoints = new TimedPoints(Configuration.PointKeepAliveTimeSpan);
            TimedPointsByControl = new TimedControlsWithPoint(Configuration.PointKeepAliveTimeSpan, this);
            GazeTimePerControl = new GazeTimePerControl(TimeSpan.FromMilliseconds(100), this);
            _selectionFilter = new TimeSpanWithMinimumSelectionsDuringTimeFrameSelectionFilter(this, 
                TimeSpan.FromMilliseconds(GazeTimeMilliseconds), 10);
            _averagingFilter = new AveragingLastNpointsWithinTimeSpanFilter(3,TimeSpan.FromMilliseconds(70));

            eventAggregator.GetEvent<NewCoordinateEvent>().Subscribe(OnNewCoordinate);
            Closing += (sender, args) => _eventAggregator.GetEvent<NewCoordinateEvent>().Unsubscribe(OnNewCoordinate);
        }

        public static readonly DependencyProperty GazeTimeMillisecondsProperty = DependencyProperty.Register(
            "GazeTimeMilliseconds", typeof(int), typeof(MainWindow), new PropertyMetadata(Configuration.InitialGazeTime));

        public int GazeTimeMilliseconds
        {
            get { return (int) GetValue(GazeTimeMillisecondsProperty); }
            set { SetValue(GazeTimeMillisecondsProperty, value); }
        }

        private TimedPoints RecentGazePoints { get; set; }

        private TimedControlsWithPoint TimedPointsByControl { get; set; }

        private GazeTimePerControl GazeTimePerControl { get; set; }

        private void OnNewCoordinate(Point point)
        {
            //RecentGazePoints =
            //    RecentGazePoints.SkipWhile(tp =>  DateTime.Now - tp.Key > Configuration.PointKeepAliveTimeSpan );
            // TODO: Decouple filtering. Use a piping mechanism.
            /* Filters & Point/Control Processing Design Decision:
            A change is never expected to be done in-place.
            -You should always either return or out a copy of
            -either the modified instance or the same instance
            -if unmodified.
            SelectionFilters have the same rules as filters and
            may also activate zero or more SelectableControls.
            */

            RecentGazePoints.Maintain();
            RecentGazePoints.AddPoint(point);
            var averagePoint = _averagingFilter.Filter(RecentGazePoints);
            if (averagePoint == null) return;
            point = averagePoint.Value;
            TimedPointsByControl.Maintain();
            TimedPointsByControl.AddPoint(point);
            GazeTimePerControl.Maintain();
            GazeTimePerControl.AddPoint(point);
            TimedPointsByControl = _selectionFilter.SelectAndUpdate(TimedPointsByControl);
        }

        private void MetroWindow_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //// Retrieve the coordinate of the mouse position.
            //Point pt = e.GetPosition((UIElement)sender);

            //// Clear the contents of the list used for hit test results.
            //_selectableControlsSeen.Clear();

            //// Set up a callback to receive the hit test result enumeration.
            //VisualTreeHelper.HitTest(Application.Current.MainWindow, null,
            //    new HitTestResultCallback(FindSelectableControlSeen),
            //    new PointHitTestParameters(pt));

            //// Perform actions on the hit test results list.
            //if (_selectableControlsSeen.Count > 0)
            //{
            //    foreach (var o in _selectableControlsSeen)
            //    {
            //        //System.Diagnostics.Debug.Write(o.ToString());
            //        //var border = o as Border;
            //        //var tb = border?.Child as TextBlock;
            //        //if (tb == null) continue;
            //        //System.Diagnostics.Debug.Write(tb.Text);


            //        //var el = o;
            //        //while (el != null && !(el is SuggestionControl))
            //        //    el = VisualTreeHelper.GetParent(el);
            //        //var sc = el as SuggestionControl;
            //        //var tb = sc?.Block as TextBlock;
            //        //if (tb == null) continue;
            //        //System.Diagnostics.Debug.Write(tb.Text);

            //        var el = o;
            //        while (el != null && !(el is Button))
            //            el = VisualTreeHelper.GetParent(el);
            //        var button = el as ContentControl;
            //        if (button == null) continue;
            //        button.Content = "No me veas!";
            //        //System.Diagnostics.Debug.Write(button.Content.ToString());
            //    }
            //}
        }
    }
}

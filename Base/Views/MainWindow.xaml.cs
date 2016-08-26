using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Prism.Events;
using TalkingKeyboard.Infrastructure;
using TalkingKeyboard.Infrastructure.Controls;

namespace TalkingKeyboard.Shell.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly IEventAggregator _eventAggregator;

        public MainWindow(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            _eventAggregator = eventAggregator;

            eventAggregator.GetEvent<NewCoordinateEvent>().Subscribe(OnNewCoordinate);
            Closing += (sender, args) => _eventAggregator.GetEvent<NewCoordinateEvent>().Unsubscribe(OnNewCoordinate);
        }

        private void OnNewCoordinate(Point point)
        {
            Dispatcher.Invoke(() =>
            {
                var window = Application.Current.MainWindow;
                if (window == null) return;
                var pt = window.PointFromScreen(point);
                _selectableControlsSeen.Clear();
                
                VisualTreeHelper.HitTest(Application.Current.MainWindow, null,
                    FindSelectableControlsSeen,
                    new PointHitTestParameters(pt));
                
                if (_selectableControlsSeen.Count <= 0) return;
                foreach (var o in _selectableControlsSeen)
                {
                    var selectableControl = o as SelectableControl;
                    selectableControl?.Command?.Execute(selectableControl?.CommandParameter);
                }
            });
        }

        private readonly List<DependencyObject> _selectableControlsSeen = new List<DependencyObject>();

        public HitTestResultBehavior FindSelectableControlsSeen(HitTestResult result)
        {
            var visualHit = result.VisualHit;
            while (visualHit != null && !(visualHit is SelectableControl))
                visualHit = VisualTreeHelper.GetParent(visualHit);
            if (visualHit != null) _selectableControlsSeen.Add(visualHit);

            // Set the behavior to return visuals at all z-order levels.
            return HitTestResultBehavior.Continue;
        }

        private void MetroWindow_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // Retrieve the coordinate of the mouse position.
            Point pt = e.GetPosition((UIElement)sender);

            // Clear the contents of the list used for hit test results.
            _selectableControlsSeen.Clear();

            // Set up a callback to receive the hit test result enumeration.
            VisualTreeHelper.HitTest(Application.Current.MainWindow, null,
                new HitTestResultCallback(FindSelectableControlsSeen),
                new PointHitTestParameters(pt));

            // Perform actions on the hit test results list.
            if (_selectableControlsSeen.Count > 0)
            {
                foreach (var o in _selectableControlsSeen)
                {
                    //System.Diagnostics.Debug.Write(o.ToString());
                    //var border = o as Border;
                    //var tb = border?.Child as TextBlock;
                    //if (tb == null) continue;
                    //System.Diagnostics.Debug.Write(tb.Text);


                    //var el = o;
                    //while (el != null && !(el is SuggestionControl))
                    //    el = VisualTreeHelper.GetParent(el);
                    //var sc = el as SuggestionControl;
                    //var tb = sc?.Block as TextBlock;
                    //if (tb == null) continue;
                    //System.Diagnostics.Debug.Write(tb.Text);

                    var el = o;
                    while (el != null && !(el is Button))
                        el = VisualTreeHelper.GetParent(el);
                    var button = el as ContentControl;
                    if (button == null) continue;
                    button.Content = "No me veas!";
                    //System.Diagnostics.Debug.Write(button.Content.ToString());
                }
            }
        }
    }
}

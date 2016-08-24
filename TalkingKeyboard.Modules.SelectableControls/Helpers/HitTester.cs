using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TalkingKeyboard.Modules.SelectableControls.Helpers
{
    public static class HitTester
    {
        public static HitTestResult Test(object sender, MouseButtonEventArgs e, FrameworkElement element)
        {
            // Retrieve the coordinate of the mouse position.
            var point = e.GetPosition((UIElement)sender);

            // Perform the hit test against a given portion of the visual object tree.
            return Test(point, element);
        }

        public static HitTestResult Test(Point point, FrameworkElement element)
        {
            return VisualTreeHelper.HitTest(element, point);
        }
    }
}
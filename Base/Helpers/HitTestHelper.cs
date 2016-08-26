using System.Windows;
using System.Windows.Media;
using TalkingKeyboard.Infrastructure.Controls;

namespace TalkingKeyboard.Shell.Helpers
{
    /* Not thread safe, but only makes sense to be called from UI Thread anyway. */
    public static class HitTestHelper
    {
        private static SelectableControl _selectableControlSeen;
        public static SelectableControl SelectableControlUnderPoint(Point point, Window window)
        {
            if (PresentationSource.FromVisual(window) == null) return null;
            var pt = window.PointFromScreen(point);

            _selectableControlSeen = null;
            VisualTreeHelper.HitTest(window, null,
                FindSelectableControlSeen,
                new PointHitTestParameters(pt));
            return _selectableControlSeen;
        }

        public static HitTestResultBehavior FindSelectableControlSeen(HitTestResult result)
        {
            var visualHit = result.VisualHit;
            while (visualHit != null && !(visualHit is SelectableControl))
                visualHit = VisualTreeHelper.GetParent(visualHit);

            if (visualHit != null)
            {
                _selectableControlSeen = visualHit as SelectableControl;
                return HitTestResultBehavior.Stop;
            }
            else
            {
                return HitTestResultBehavior.Continue;
            }
        }
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HitTestHelper.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the HitTestHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TalkingKeyboard.Infrastructure.Helpers
{
    using System.Windows;
    using System.Windows.Media;

    using TalkingKeyboard.Infrastructure.Controls;

    /* Not thread safe, but only makes sense to be called from UI Thread anyway. */
    public static class HitTestHelper
    {
        private static SelectableControl _selectableControlSeen;

        public static HitTestResultBehavior FindSelectableControlSeen(HitTestResult result)
        {
            var visualHit = result.VisualHit;
            while ((visualHit != null) && !(visualHit is SelectableControl))
            {
                visualHit = VisualTreeHelper.GetParent(visualHit);
            }

            if (visualHit != null)
            {
                _selectableControlSeen = visualHit as SelectableControl;
                return HitTestResultBehavior.Stop;
            }

            return HitTestResultBehavior.Continue;
        }

        public static SelectableControl SelectableControlUnderPoint(Point point, Window window)
        {
            _selectableControlSeen = null;
            window.Dispatcher.Invoke(
                () =>
                    {
                        if (PresentationSource.FromVisual(window) == null)
                        {
                            return;
                        }

                        var pt = window.PointFromScreen(point);

                        VisualTreeHelper.HitTest(
                            window,
                            null,
                            FindSelectableControlSeen,
                            new PointHitTestParameters(pt));
                    });
            return _selectableControlSeen;
        }
    }
}
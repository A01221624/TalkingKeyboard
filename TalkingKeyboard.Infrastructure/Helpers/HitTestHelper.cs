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

    /// <summary>
    ///     This helper class contains static methods to perform hit testing.
    /// </summary>
    /// <remarks>
    ///     Not thread safe, but only makes sense to be called from UI Thread anyway.
    /// </remarks>
    public static class HitTestHelper
    {
        private static SelectableControl _selectableControlSeen;

        /// <summary>
        ///     Finds the selectable control on which a gaze-point falls and populates a field in the class.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>
        ///     <see cref="HitTestResultBehavior.Stop" /> as soon as the first <see cref="SelectableControl" /> under the point is
        ///     found. Otherwise it returns <see cref="HitTestResultBehavior.Continue" />.
        /// </returns>
        public static HitTestResultBehavior FindSelectableControlSeen(HitTestResult result)
        {
            var visualHit = result.VisualHit;
            while ((visualHit != null) && !(visualHit is SelectableControl))
            {
                visualHit = VisualTreeHelper.GetParent(visualHit);
            }

            if (visualHit == null)
            {
                return HitTestResultBehavior.Continue;
            }

            _selectableControlSeen = visualHit as SelectableControl;
            return HitTestResultBehavior.Stop;
        }

        /// <summary>
        /// Finds the <see cref="SelectableControl"/> under the given <see cref="Point"/> in the given <see cref="Window"/>.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="window">The window.</param>
        /// <returns>The <see cref="SelectableControl"/> under the point.</returns>
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
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectableState.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the SelectableState type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.Enums
{
    /// <summary>
    ///     This enumerator describes the possible selection-states for the gaze-based selection state machine (or, potentially
    ///     any other point-dwell selection state machine).
    /// </summary>
    public enum SelectableState
    {
        /// <summary>
        ///     Awaiting for points to fall on control.
        /// </summary>
        Idle,

        /// <summary>
        ///     A point has recently fallen on the control; waiting for a dead-time before animation begins.
        /// </summary>
        /// <remarks>
        ///     By not letting animations start immediately, distractions and midas-touch issues are reduced (e.g. when traveling
        ///     over controls).
        /// </remarks>
        SeenButWaiting,

        /// <summary>
        ///     The animation is currently running (i.e. selection is underway and the user is aware of it).
        /// </summary>
        AnimationRunning,

        /// <summary>
        ///     The animation is on hold as the user looked elsewhere while selection was underway. The selection progress will be
        ///     kept for a given amount of time.
        /// </summary>
        /// <remarks>
        ///     This helps in case the point stream is unreliable as well as for users who tend to move their eyes too much even
        ///     when focusing on a point.
        /// </remarks>
        AnimationOnHold,

        /// <summary>
        ///     The control has been recently selected and a dead-time is underway before returning to idle.
        /// </summary>
        /// <remarks>
        ///     This helps introduce a time between selections of the same key, as the eyes will linger shortly on a control after
        ///     selecting it and it could result in midas-touch.
        /// </remarks>
        RecentlySelected
    }
}
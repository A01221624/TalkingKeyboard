// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISelectableControlViewModel.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TalkingKeyboard.Infrastructure.Controls
{
    using System;
    using System.Windows.Input;
    using System.Windows.Media.Animation;

    using TalkingKeyboard.Infrastructure.Enums;

    /// <summary>
    /// The SelectableControlViewModel interface.
    /// </summary>
    public interface ISelectableControlViewModel : ICommandSource
    {
        /// <summary>
        /// Gets or sets the animation.
        /// </summary>
        Storyboard Animation { get; set; }

        /// <summary>
        /// Gets or sets the current gaze time span.
        /// </summary>
        TimeSpan CurrentGazeTimeSpan { get; set; }

        /// <summary>
        /// Gets or sets the gaze keep alive time span.
        /// </summary>
        TimeSpan GazeKeepAliveTimeSpan { get; set; }

        /// <summary>
        /// Gets or sets the gaze time span before animation begins.
        /// </summary>
        TimeSpan GazeTimeSpanBeforeAnimationBegins { get; set; }

        /// <summary>
        /// Gets or sets the gaze time span before cool-down.
        /// </summary>
        TimeSpan GazeTimeSpanBeforeCooldown { get; set; }

        /// <summary>
        /// Gets or sets the gaze time span before selection occurs.
        /// </summary>
        TimeSpan GazeTimeSpanBeforeSelectionOccurs { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selectable.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is selectable; otherwise, <c>false</c>.
        /// </value>
        bool IsSelectable { get; set; }

        /// <summary>
        /// Gets or sets the last seen time.
        /// </summary>
        DateTime LastSeenTime { get; set; }

        /// <summary>
        /// Gets or sets the last selected time.
        /// </summary>
        DateTime LastSelectedTime { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        SelectableState State { get; set; }

        /// <summary>
        /// Executes the Command assigned to the control.
        /// </summary>
        void Select();
    }
}
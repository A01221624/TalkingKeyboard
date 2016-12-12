using System;
using System.ComponentModel;

namespace TalkingKeyboard.Infrastructure.Controls
{
    public interface ISelectableControlModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the required gaze time span.
        /// </summary>
        /// <value>
        /// The required gaze time span.
        /// </value>
        TimeSpan RequiredGazeTimeSpan { get; set; }
    }
}
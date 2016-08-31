using System;
using System.Windows.Input;
using System.Windows.Media.Animation;
using TalkingKeyboard.Infrastructure.Enums;

namespace TalkingKeyboard.Infrastructure.Controls
{
    public interface ISelectableControlViewModel : ICommandSource
    {
        DateTime LastSelectedTime { get; set; }
        DateTime LastSeenTime { get; set; }
        TimeSpan GazeTimeSpan { get; set; }
        SelectableState State { get; set; }
        TimeSpan GazeKeepAliveTimeSpan { get; set; }
        TimeSpan GazeTimeSpanBeforeAnimationBegins { get; set; }
        TimeSpan GazeTimeSpanBeforeSelectionOccurs { get; set; }
        TimeSpan GazeTimeSpanBeforeCooldown { get; set; }
        Storyboard Animation { get; set; }
        void PlayAnimation();
        void PauseAnimation();
        void StopAnimation();
        void ResumeAnimation();
        void Select();


    }
}
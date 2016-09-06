using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace TalkingKeyboard.Infrastructure.Controls
{
    /// <summary>
    ///     Interaction logic for SelectableControl.xaml
    /// </summary>
    public abstract class SelectableControl : ContentControl, ICommandSource, IComparable
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", typeof(ICommand), typeof(SelectableControl), new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            "CommandParameter", typeof(object), typeof(SelectableControl), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty CommandTargetProperty = DependencyProperty.Register(
            "CommandTarget", typeof(IInputElement), typeof(SelectableControl),
            new PropertyMetadata(default(IInputElement)));

        public static readonly DependencyProperty AnimationProperty = DependencyProperty.Register(
            "Animation", typeof(Storyboard), typeof(SelectableControl), new PropertyMetadata(default(Storyboard)));

        public Storyboard Animation
        {
            get { return (Storyboard) GetValue(AnimationProperty); }
            set { SetValue(AnimationProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand) GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public IInputElement CommandTarget
        {
            get { return (IInputElement) GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }

        public int CompareTo(object obj)
        {
            var other = obj as SelectableControl;
            if (other == null) return -1;
            if (other.Uid == Uid) return 0;
            return 1;
        }

        public void PlayAnimation()
        {
            Animation.Begin();
        }

        public void PauseAnimation()
        {
            Animation.Pause();
        }

        public void StopAnimation()
        {
            Animation.Stop();
        }

        public void ResumeAnimation()
        {
            Animation.Resume();
        }

        public void Select()
        {
            if (Command == null) return;
            var canExecute = Command.CanExecute(CommandParameter);
            if (!canExecute) return;
            Command?.Execute(CommandParameter);
            SystemSounds.Hand.Play();
        }
    }
}
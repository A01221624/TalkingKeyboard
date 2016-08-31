using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TalkingKeyboard.Infrastructure.Enums;

namespace TalkingKeyboard.Infrastructure.Controls
{
    /// <summary>
    /// Interaction logic for SelectableControl.xaml
    /// </summary>
    public abstract class SelectableControl : ContentControl, ICommandSource, IComparable
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", typeof(ICommand), typeof(SelectableControl), new PropertyMetadata(default(ICommand)));

        public ICommand Command
        {
            get { return (ICommand) GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            "CommandParameter", typeof(object), typeof(SelectableControl), new PropertyMetadata(default(object)));

        public object CommandParameter
        {
            get { return (object) GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly DependencyProperty CommandTargetProperty = DependencyProperty.Register(
            "CommandTarget", typeof(IInputElement), typeof(SelectableControl), new PropertyMetadata(default(IInputElement)));

        public IInputElement CommandTarget
        {
            get { return (IInputElement) GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }

        public static readonly DependencyProperty AnimationProperty = DependencyProperty.Register(
            "Animation", typeof(Storyboard), typeof(SelectableControl), new PropertyMetadata(default(Storyboard)));

        public Storyboard Animation
        {
            get { return (Storyboard) GetValue(AnimationProperty); }
            set { SetValue(AnimationProperty, value); }
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
            Command?.Execute(CommandParameter);
            System.Media.SystemSounds.Hand.Play();
        }

        public int CompareTo(object obj)
        {
            var other = obj as SelectableControl;
            if (other == null) return -1;
            if (other.Uid == this.Uid) return 0;
            return 1;
        }
    }
}

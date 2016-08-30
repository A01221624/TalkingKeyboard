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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        public static readonly DependencyProperty LastSelectedTimeProperty = DependencyProperty.Register(
            "LastSelectedTime", typeof(DateTime), typeof(SelectableControl), new PropertyMetadata(default(DateTime)));

        public DateTime LastSelectedTime
        {
            get { return (DateTime) GetValue(LastSelectedTimeProperty); }
            set { SetValue(LastSelectedTimeProperty, value); }
        }

        public void Select()
        {
            Command?.Execute(CommandParameter);
            LastSelectedTime = DateTime.Now;
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

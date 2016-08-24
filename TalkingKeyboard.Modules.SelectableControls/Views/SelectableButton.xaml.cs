using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using Prism.Events;
using TalkingKeyboard.Infrastructure;
using TalkingKeyboard.Modules.SelectableControls.Helpers;
using TalkingKeyboard.Modules.SelectableControls.ViewModels;

namespace TalkingKeyboard.Modules.SelectableControls.Views
{
    /// <summary>
    /// Interaction logic for SelectableButton
    /// </summary>
    public partial class SelectableButton : UserControl
    {
        private IEventAggregator _eventAggregator;
        public SelectableButton()
        {
            InitializeComponent();
            Application.Current.MainWindow.ContentRendered += (sender, args) =>
            {
                    var context = (SelectableButtonViewModel) DataContext;
                _eventAggregator = context.EventAggregator;
                _eventAggregator.GetEvent<NewCoordinateEvent>().Subscribe(OnNewCoordinate);
            };
            Dispatcher.ShutdownStarted += (sender, args) => _eventAggregator.GetEvent<NewCoordinateEvent>().Unsubscribe(OnNewCoordinate);
        }

        private void OnNewCoordinate(Point point)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                if (!Dispatcher.HasShutdownStarted && VisualTreeHelper.HitTest(Button, Button.PointFromScreen(point)) != null)
                {
                    Button.Command?.Execute(null);
                }
            }));
        }
    }
}

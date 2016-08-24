using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Threading;
using Prism.Events;
using Prism.Modularity;

namespace TalkingKeyboard.Infrastructure.Behaviors
{
    public class SelectableBehavior : Behavior<FrameworkElement>
    {
        private readonly IEventAggregator _eventAggregator;

        public SelectableBehavior()
        {
            _eventAggregator = Infrastructure.Constants.RegionNames.EventAggregator;
            
        }

        protected override void OnAttached()
        {
            _eventAggregator.GetEvent<NewCoordinateEvent>().Subscribe(point =>
            {
                this.Dispatcher.Invoke(new Action(delegate
                {
                    var result = HitTester.Test(AssociatedObject, point);
                    if (result == null) return;
                    var textBlock = AssociatedObject as TextBlock;
                    if (textBlock == null) return;
                    textBlock.Text = "Holi";
                }));
            });
            //Window parent = Application.Current.MainWindow;
            //var textBlock = AssociatedObject as TextBlock;
            //if (textBlock == null) return;
            //textBlock.MouseEnter += (sender, args) =>
            //{
            //    textBlock.Text = "Holi";
            //};
        }
    }
}

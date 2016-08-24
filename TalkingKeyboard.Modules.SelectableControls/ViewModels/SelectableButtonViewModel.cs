using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Prism.Events;
using TalkingKeyboard.Infrastructure;
using TalkingKeyboard.Modules.SelectableControls.Helpers;
using TalkingKeyboard.Modules.SelectableControls.Views;

namespace TalkingKeyboard.Modules.SelectableControls.ViewModels
{
    public class SelectableButtonViewModel : BindableBase
    {
        public IEventAggregator EventAggregator { get; set; }
        public Button Button { get; set; }

        public SelectableButtonViewModel(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
        }
    }
}

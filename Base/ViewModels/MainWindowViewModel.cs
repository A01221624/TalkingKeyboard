﻿using Prism.Events;
using Prism.Mvvm;
using TalkingKeyboard.Infrastructure;

namespace TalkingKeyboard.Shell.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Unity Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
        }
    }
}

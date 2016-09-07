using System.Collections.Generic;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace TalkingKeyboard.Shell.ViewModels
{
    using TalkingKeyboard.Infrastructure.Constants;

    public class MainWindowViewModel : BindableBase
    {
        private readonly List<string> _knownBoards;
        private readonly IRegionManager _regionManager;
        private int _currentViewIndex; // Must be set to index of initial board.
        private string _title = "Prism Unity Application";

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _knownBoards = new List<string>
            {
                ViewNames.QwertySpanishMultiKeyboard,
                ViewNames.QwertySpanishSingleKeyboard
            };

            ChangeViewToLeftCommand = new DelegateCommand(() =>
            {
                if (--_currentViewIndex < 0) _currentViewIndex = _knownBoards.Count - 1;
                _regionManager.RequestNavigate(RegionNames.BoardViewRegion,
                    _knownBoards[_currentViewIndex]);
            });
            ChangeViewToRightCommand = new DelegateCommand(() =>
            {
                if (++_currentViewIndex >= _knownBoards.Count) _currentViewIndex = 0;
                _regionManager.RequestNavigate(RegionNames.BoardViewRegion,
                    _knownBoards[_currentViewIndex]);
            });
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ICommand ChangeViewToRightCommand { get; set; }

        public ICommand ChangeViewToLeftCommand { get; set; }
    }
}
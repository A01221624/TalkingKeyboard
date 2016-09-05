using System;
using System.Collections.ObjectModel;
using TalkingKeyboard.Infrastructure.Models;
using TalkingKeyboard.Infrastructure.ServiceInterfaces;

namespace TalkingKeyboard.Modules.MultiKeyBoard.Model
{
    public class MultikeyTextModel : IMultiKeyTextModel
    {
        private string _currentMultiText;

        public string CurrentMultiText
        {
            get { return _currentMultiText; }
            set { _currentMultiText = value; }
        }
    }
}
using TalkingKeyboard.Infrastructure.Models;

namespace TalkingKeyboard.Modules.MultiKeyBoard.Model
{
    public class MultikeyTextModel : IMultiKeyTextModel
    {
        public string CurrentMultiCharacterText { get; set; }
    }
}
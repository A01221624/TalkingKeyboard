using System.Collections.Concurrent;
using System.Windows;

namespace TalkingKeyboard.Infrastructure.ServiceInterfaces
{
    public interface IWindowKnower
    {
        ConcurrentBag<Window> Windows { get; set; }
    }
}
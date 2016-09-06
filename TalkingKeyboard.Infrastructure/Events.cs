using System.Windows;
using Prism.Events;

namespace TalkingKeyboard.Infrastructure
{
    public class NewCoordinateEvent : PubSubEvent<Point>
    {
    }

    public class TextUpdatedEvent : PubSubEvent
    {
    }

    public class MultiTextUpdatedEvent : PubSubEvent
    {
    }
}
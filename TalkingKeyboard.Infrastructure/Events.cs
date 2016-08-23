using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TalkingKeyboard.Infrastructure
{
    public class NewCoordinateEvent : PubSubEvent<Point>
    {
    }
}

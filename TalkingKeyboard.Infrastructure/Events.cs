using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkingKeyboard.Infrastructure
{
    public class NewCoordinateEvent : PubSubEvent<Tuple<int,int>>
    {
    }
}

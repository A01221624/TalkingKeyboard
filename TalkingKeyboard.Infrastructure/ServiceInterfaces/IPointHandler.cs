using System.Windows;

namespace TalkingKeyboard.Infrastructure.ServiceInterfaces
{
    public interface IPointHandler
    {
        void ProcessPoint(Point point);
    }
}
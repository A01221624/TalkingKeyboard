using System;

namespace TalkingKeyboard.Infrastructure
{
    public static class Configuration
    {
        public static int InitialGazeTime => 300;

        public static TimeSpan PointKeepAliveTimeSpan => TimeSpan.FromSeconds(5);
    }
}
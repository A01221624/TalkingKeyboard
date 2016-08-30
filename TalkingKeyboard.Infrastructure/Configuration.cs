using System;

namespace TalkingKeyboard.Infrastructure
{
    public static class Configuration
    {
        public static int InitialGazeTime => 500;

        public static TimeSpan PointKeepAliveTimeSpan => TimeSpan.FromSeconds(1);
    }
}
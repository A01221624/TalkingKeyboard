using System;

namespace TalkingKeyboard.Infrastructure
{
    public static class Configuration
    {
        public static TimeSpan InitialRequiredGazeTime => TimeSpan.FromMilliseconds(500);
        public static TimeSpan GazeTimeSpanBeforeAnimationBegins => TimeSpan.FromMilliseconds(50);
        public static TimeSpan GazeKeepAliveTimeSpan => TimeSpan.FromMilliseconds(100);
        private static TimeSpan GazeCoolDownTimeSpan => TimeSpan.FromMilliseconds(50);

        public static TimeSpan GazeTimeSpanBeforeSelectionOccurs
            => InitialRequiredGazeTime + GazeTimeSpanBeforeAnimationBegins;

        public static TimeSpan GazeTimeSpanBeforeCooldownazeTimeSpanBeforeSelectionOccurs
            => GazeTimeSpanBeforeSelectionOccurs + GazeCoolDownTimeSpan;

        public static TimeSpan PointKeepAliveTimeSpan => TimeSpan.FromSeconds(1);
    }
}
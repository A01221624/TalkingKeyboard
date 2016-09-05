using System;

namespace TalkingKeyboard.Infrastructure
{
    public static class Configuration
    {
        public static TimeSpan InitialRequiredGazeTime => TimeSpan.FromMilliseconds(500);
        public static TimeSpan GazeTimeSpanBeforeAnimationBegins => TimeSpan.FromMilliseconds(100);
        public static TimeSpan GazeKeepAliveTimeSpan => TimeSpan.FromMilliseconds(70);
        private static TimeSpan GazeCoolDownTimeSpan => TimeSpan.FromMilliseconds(200);

        public static TimeSpan GazeTimeSpanBeforeSelectionOccurs
            => InitialRequiredGazeTime + GazeTimeSpanBeforeAnimationBegins;

        public static TimeSpan GazeTimeSpanBeforeCooldownazeTimeSpanBeforeSelectionOccurs
            => GazeTimeSpanBeforeSelectionOccurs + GazeCoolDownTimeSpan;

        public static TimeSpan PointKeepAliveTimeSpan => TimeSpan.FromSeconds(1);
        public static string DefaultDictionaryFilePath => @"Resources\SpanishSpain.dic";
    }
}
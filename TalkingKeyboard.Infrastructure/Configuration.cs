// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Configuration.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Contains configuration constants.
// </summary>
// <remarks>
//   It could be useful to change and use Windows-based configuration for modifying externally.
// </remarks>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure
{
    using System;

    public static class Configuration
    {
        /// <summary>
        ///     Gets the default dictionary file path.
        /// </summary>
        /// <value>
        ///     The default dictionary file path.
        /// </value>
        public static string DefaultDictionaryFilePath => @"Resources\SpanishSpain.dic";

        /// <summary>
        ///     Gets the default gaze keep alive time span.
        /// </summary>
        /// <value>
        ///     The gaze keep alive time span.
        /// </value>
        public static TimeSpan GazeKeepAliveTimeSpan => TimeSpan.FromMilliseconds(70);

        /// <summary>
        ///     Gets the default gaze time span before animation begins.
        /// </summary>
        /// <value>
        ///     The gaze time span before animation begins.
        /// </value>
        public static TimeSpan GazeTimeSpanBeforeAnimationBegins => TimeSpan.FromMilliseconds(100);

        /// <summary>
        ///     Gets the default gaze time span before cool-down occurs.
        /// </summary>
        /// <value>
        ///     The gaze time span before cool-down occurs.
        /// </value>
        public static TimeSpan GazeTimeSpanBeforeCooldownOccurs
            => GazeTimeSpanBeforeSelectionOccurs + GazeCoolDownTimeSpan;

        /// <summary>
        ///     Gets the default gaze time span before selection occurs.
        /// </summary>
        /// <value>
        ///     The gaze time span before selection occurs.
        /// </value>
        public static TimeSpan GazeTimeSpanBeforeSelectionOccurs
            => InitialRequiredGazeTime + GazeTimeSpanBeforeAnimationBegins;

        /// <summary>
        ///     Gets the initial (default) required gaze time.
        /// </summary>
        /// <value>
        ///     The initial required gaze time.
        /// </value>
        public static TimeSpan InitialRequiredGazeTime => TimeSpan.FromMilliseconds(500);

        /// <summary>
        ///     Gets the default point keep alive time span.
        /// </summary>
        /// <value>
        ///     The point keep alive time span.
        /// </value>
        public static TimeSpan PointKeepAliveTimeSpan => TimeSpan.FromSeconds(1);

        /// <summary>
        ///     Gets the default gaze cool-down time span.
        /// </summary>
        /// <value>
        ///     The gaze cool-down time span.
        /// </value>
        private static TimeSpan GazeCoolDownTimeSpan => TimeSpan.FromMilliseconds(200);
    }
}
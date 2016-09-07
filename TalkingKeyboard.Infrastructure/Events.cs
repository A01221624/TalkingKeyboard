// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Events.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure
{
    using System.Windows;

    using Prism.Events;

    public class Events
    {
        /// <summary>
        ///     Event to be raised when the current multi-character text is updated.
        /// </summary>
        /// <seealso cref="Prism.Events.PubSubEvent" />
        public class MultiTextUpdatedEvent : PubSubEvent
        {
        }

        /// <summary>
        ///     Event to be raised when a new coordinate is received (e.g. Gaze detection device provides new point).
        /// </summary>
        /// <seealso cref="Prism.Events.PubSubEvent{Point}" />
        public class NewCoordinateEvent : PubSubEvent<Point>
        {
        }

        /// <summary>
        ///     Event to be raised when the current text is updated.
        /// </summary>
        /// <seealso cref="Prism.Events.PubSubEvent" />
        public class TextUpdatedEvent : PubSubEvent
        {
        }
    }
}
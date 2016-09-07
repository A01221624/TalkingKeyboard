// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMaintainable.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the IMaintainable type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.DataContainers
{
    public interface IMaintainable
    {
        /// <summary>
        ///     Performs any regular maintenance tasks such as
        ///     clean-up of old elements.
        /// </summary>
        void Maintain();
    }
}
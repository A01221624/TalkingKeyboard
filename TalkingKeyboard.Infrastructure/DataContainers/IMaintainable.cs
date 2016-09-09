// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMaintainable.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.DataContainers
{
    /// <summary>
    ///     Defines the IMaintainable interface used to make collections maintainable (e.g. eliminating old elements).
    /// </summary>
    public interface IMaintainable
    {
        /// <summary>
        ///     Performs any regular maintenance tasks such as
        ///     clean-up of old elements.
        /// </summary>
        void Maintain();
    }
}
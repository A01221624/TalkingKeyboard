// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWindowKnower.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the IWindowKnower interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.ServiceInterfaces
{
    using System.Collections.Concurrent;
    using System.Windows;

    public interface IWindowKnower
    {
        /// <summary>
        ///     Gets or sets the known windows.
        /// </summary>
        /// <value>
        ///     The known windows.
        /// </value>
        ConcurrentBag<Window> KnownWindows { get; set; }
    }
}
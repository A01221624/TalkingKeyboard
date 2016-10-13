// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPointHandler.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the IPointHandler interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.ServiceInterfaces
{
    using System.Windows;

    /// <summary>
    ///     Interface for coordinate point-based services.
    /// </summary>
    public interface IPointHandler
    {
        /// <summary>
        ///     Processes the point.
        /// </summary>
        /// <param name="point">The point to be processed.</param>
        void ProcessPoint(Point point);
    }
}
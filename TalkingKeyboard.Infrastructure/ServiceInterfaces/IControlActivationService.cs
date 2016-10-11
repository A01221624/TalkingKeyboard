// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IControlActivationService.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the IControlActivationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.ServiceInterfaces
{
    /// <summary>
    ///     Interface for control activation services.
    /// </summary>
    /// <seealso cref="TalkingKeyboard.Infrastructure.ServiceInterfaces.IPointHandler" />
    /// <seealso cref="TalkingKeyboard.Infrastructure.ServiceInterfaces.IWindowKnower" />
    public interface IControlActivationService : IPointHandler, IWindowKnower
    {
    }
}
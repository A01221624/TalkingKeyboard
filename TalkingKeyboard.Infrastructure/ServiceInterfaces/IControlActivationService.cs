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
    public interface IControlActivationService : IPointHandler, IWindowKnower
    {
    }
}
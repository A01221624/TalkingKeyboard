// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceLocations.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.Constants
{
    using System.IO;

    /// <summary>
    ///     Resource location constants (e.g. dictionary paths).
    /// </summary>
    public static class ResourceLocations
    {
        /// <summary>
        ///     The default dictionary location.
        /// </summary>
        /// <remarks>
        ///     Original project is for Spanish user. Therefore the default dictionary is Spanish.
        /// </remarks>
        public static string DefaultDictionaryLocation => SpanishDictionaryLocation;

        /// <summary>
        ///     The Spanish dictionary location.
        /// </summary>
        public static string SpanishDictionaryLocation => Path.GetFullPath(@"Resources\SpanishSpain.dic");
    }
}
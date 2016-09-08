// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectionFilter.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.ByGazeTimePointProcessor.Filters
{
    using System.Windows;

    /// <summary>
    ///     Defines the abstract SelectionFilter type. This type filters the received points as necessary and executes any
    ///     commands (selects) accordingly.
    ///     After filtering and selecting, the original point collection may (should) be updated if necessary to avoid multiple
    ///     selections.
    /// </summary>
    /// <typeparam name="T">The type of the point collection.</typeparam>
    public abstract class SelectionFilter<T>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SelectionFilter{T}" /> class.
        /// </summary>
        /// <param name="window">The window.</param>
        protected SelectionFilter(Window window)
        {
            this.Window = window;
        }

        /// <summary>
        ///     Gets or sets the window on which the controls for the points are located.
        /// </summary>
        /// <value>
        ///     The window on which the controls for the points are located.
        /// </value>
        protected Window Window { get; set; }

        /// <summary>
        ///     Filters the collection, selects accordingly and returns an updated collection.
        /// </summary>
        /// <param name="pointCollection">The point collection.</param>
        /// <returns>The updated point collection.</returns>
        public abstract T SelectAndUpdate(T pointCollection);
    }
}
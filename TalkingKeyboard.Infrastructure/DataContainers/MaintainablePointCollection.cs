// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MaintainablePointCollection.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the MaintainablePointCollection type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.DataContainers
{
    using System;
    using System.Collections.Concurrent;
    using System.Windows;

    /// <summary>
    ///     Defines the MaintainablePointCollection abstract class used as a base for collections of points mapped to a given
    ///     object (e.g. control, time).
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    /// <seealso cref="System.Collections.Concurrent.ConcurrentDictionary{T1, T2}" />
    /// <seealso cref="TalkingKeyboard.Infrastructure.DataContainers.IMaintainable" />
    public abstract class MaintainablePointCollection<T1, T2> : ConcurrentDictionary<T1, T2>, IMaintainable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MaintainablePointCollection{T1, T2}" /> class given the point keep
        ///     alive time span.
        /// </summary>
        /// <param name="pointKeepAliveTimeSpan">The point keep alive time span.</param>
        protected MaintainablePointCollection(TimeSpan pointKeepAliveTimeSpan)
        {
            this.PointKeepAliveTimeSpan = pointKeepAliveTimeSpan;
            this.LastMaintainedTime = DateTime.Now;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MaintainablePointCollection{T1, T2}" /> class.
        /// </summary>
        protected MaintainablePointCollection()
            : this(Configuration.PointKeepAliveTimeSpan)
        {
        }

        /// <summary>
        ///     Gets or sets the time on which the collection was last maintained.
        /// </summary>
        /// <value>
        ///     The time on which the collection was last maintained.
        /// </value>
        protected DateTime LastMaintainedTime { get; set; }

        /// <summary>
        ///     Gets the maximum time for which to keep a point alive after it was obtained.
        /// </summary>
        /// <value>
        ///     The time span for which to keep a point alive after it was obtained.
        /// </value>
        protected TimeSpan PointKeepAliveTimeSpan { get; }

        /// <summary>
        ///     Adds a new point to the collection by doing the necessary processing.
        /// </summary>
        /// <param name="point">The point to be added and processed.</param>
        public abstract void AddPoint(Point point);

        /// <summary>
        ///     Removes any points older than <see cref="PointKeepAliveTimeSpan" />
        ///     from this.
        /// </summary>
        public abstract void Maintain();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TalkingKeyboard.Infrastructure.DataContainers
{
    public abstract class MaintainablePointCollection<T1,T2> : IMaintainable
    {
        protected DateTime LastMaintainedTime;
        protected TimeSpan PointKeepAliveTimeSpan;

        protected MaintainablePointCollection(TimeSpan pointKeepAliveTimeSpan)
        {
            Collection = new SortedList<T1, T2>();
            PointKeepAliveTimeSpan = pointKeepAliveTimeSpan;
            LastMaintainedTime = DateTime.Now;
        }

        public IDictionary<T1, T2> Collection { get; set; }

        /// <summary>
        ///     Removes any points older than <see cref="PointKeepAliveTimeSpan" />
        ///     from <see cref="Collection" />
        /// </summary>
        public abstract void Maintain();

        public abstract void Add(Point point);
    }
}
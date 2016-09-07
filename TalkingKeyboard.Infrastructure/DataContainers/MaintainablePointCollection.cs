using System;
using System.Collections.Concurrent;
using System.Windows;

namespace TalkingKeyboard.Infrastructure.DataContainers
{
    public abstract class MaintainablePointCollection<T1, T2> : ConcurrentDictionary<T1, T2>, IMaintainable
    {
        protected DateTime LastMaintainedTime;
        protected TimeSpan PointKeepAliveTimeSpan;

        protected MaintainablePointCollection(TimeSpan pointKeepAliveTimeSpan)
        {
            PointKeepAliveTimeSpan = pointKeepAliveTimeSpan;
            LastMaintainedTime = DateTime.Now;
        }

        protected MaintainablePointCollection() : this(Configuration.PointKeepAliveTimeSpan)
        {
        }

        /// <summary>
        ///     Removes any points older than <see cref="PointKeepAliveTimeSpan" />
        ///     from this.
        /// </summary>
        public abstract void Maintain();

        public abstract void AddPoint(Point point);


        //public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator()
        //{
        //    return Collection.GetEnumerator();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}

        //public void AddPoint(KeyValuePair<T1, T2> item)
        //{
        //    Collection.AddPoint(item.Key, item.Value);
        //}

        //public void Clear()
        //{
        //    Collection.Clear();
        //}

        //public bool Contains(KeyValuePair<T1, T2> item)
        //{
        //    return Collection.Contains(item);
        //}

        //public void CopyTo(KeyValuePair<T1, T2>[] array, int arrayIndex)
        //{
        //    var c = (IDictionary<T1, T2>) Collection;
        //    c.CopyTo(array, arrayIndex);
        //}

        //public bool Remove(KeyValuePair<T1, T2> item)
        //{
        //    return Collection.Remove(item.Key);
        //}

        //public int Count => Collection.Count;
        //public bool IsReadOnly => false;
        //public bool ContainsKey(T1 key)
        //{
        //    return Collection.ContainsKey(key);
        //}

        //public void AddPoint(T1 key, T2 value)
        //{
        //    Collection.AddPoint(key, value);
        //}

        //public bool Remove(T1 key)
        //{
        //    return Collection.Remove(key);
        //}

        //public bool TryGetValue(T1 key, out T2 value)
        //{
        //    return Collection.TryGetValue(key, out value);
        //}

        //T2 IDictionary<T1, T2>.this[T1 key]
        //{
        //    get { return Collection[key]; }
        //    set { Collection[key] = value; }
        //}

        //public ICollection<T1> Keys => Collection.Keys;
        //public ICollection<T2> Values => Collection.Values;
    }
}
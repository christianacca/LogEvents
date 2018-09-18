using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CcAcca.LogEvents.Internals
{
    public class DynamicMetricsDictionary : IDictionary<string, DynamicMetric>
    {
        private IDictionary<string, DynamicMetric> _impl = new Dictionary<string, DynamicMetric>();

        public IEnumerator<KeyValuePair<string, DynamicMetric>> GetEnumerator()
        {
            return _impl.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _impl).GetEnumerator();
        }

        public void Add(KeyValuePair<string, DynamicMetric> item)
        {
            if (string.IsNullOrWhiteSpace(item.Key))
            {
                throw new System.ArgumentException("Metric key required", nameof(item));
            }
            _impl.Add(item);
        }

        public void Clear()
        {
            _impl.Clear();
        }

        public bool Contains(KeyValuePair<string, DynamicMetric> item)
        {
            return _impl.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, DynamicMetric>[] array, int arrayIndex)
        {
            _impl.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, DynamicMetric> item)
        {
            return _impl.Remove(item);
        }

        public int Count => _impl.Count;

        public bool IsReadOnly => _impl.IsReadOnly;

        public void Add(string key, DynamicMetric value)
        {
            if (key != null && string.IsNullOrWhiteSpace(key))
            {
                throw new System.ArgumentException("Metric key required", nameof(key));
            }
            _impl.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return _impl.ContainsKey(key);
        }

        public bool Remove(string key)
        {
            return _impl.Remove(key);
        }

        public bool TryGetValue(string key, out DynamicMetric value)
        {
            return _impl.TryGetValue(key, out value);
        }

        public DynamicMetric this[string key]
        {
            get => _impl[key];
            set
            {
                if (key != null && string.IsNullOrWhiteSpace(key))
                {
                    throw new System.ArgumentException("Metric key required", nameof(key));
                }
                _impl[key] = value;
            }
        }

        public ICollection<string> Keys => _impl.Keys;

        public ICollection<DynamicMetric> Values => _impl.Values;

        /// <summary>
        /// Create a snapshot of the value from each <see cref="DynamicMetric"/>
        /// stored by this instance
        /// </summary>
        public IDictionary<string, double> Evaluate()
        {
            return this.ToDictionary(pair => pair.Key, pair => pair.Value.Value);
        }
    }
}
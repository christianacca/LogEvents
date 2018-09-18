using System.Collections;
using System.Collections.Generic;

namespace CcAcca.LogEvents.Internals
{
    internal class MetricsDictionary: IDictionary<string, double>
    {
        private IDictionary<string, double> _impl = new Dictionary<string, double>();

        public IEnumerator<KeyValuePair<string, double>> GetEnumerator()
        {
            return _impl.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _impl).GetEnumerator();
        }

        public void Add(KeyValuePair<string, double> item)
        {
            _impl.Add(item);
        }

        public void Clear()
        {
            _impl.Clear();
        }

        public bool Contains(KeyValuePair<string, double> item)
        {
            return _impl.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, double>[] array, int arrayIndex)
        {
            _impl.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, double> item)
        {
            return _impl.Remove(item);
        }

        public int Count => _impl.Count;

        public bool IsReadOnly => _impl.IsReadOnly;

        public void Add(string key, double value)
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

        public bool TryGetValue(string key, out double value)
        {
            return _impl.TryGetValue(key, out value);
        }

        public double this[string key]
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

        public ICollection<double> Values => _impl.Values;
    }
}
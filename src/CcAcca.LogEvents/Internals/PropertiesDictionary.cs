using System;
using System.Collections;
using System.Collections.Generic;

namespace CcAcca.LogEvents.Internals
{
    internal class PropertiesDictionary: IDictionary<string, string>
    {
        private IDictionary<string, string> _impl = new Dictionary<string, string>();

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _impl.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _impl).GetEnumerator();
        }

        public void Add(KeyValuePair<string, string> item)
        {
            _impl.Add(item);
        }

        public void Clear()
        {
            _impl.Clear();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            return _impl.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            _impl.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            return _impl.Remove(item);
        }

        public int Count => _impl.Count;

        public bool IsReadOnly => _impl.IsReadOnly;

        public void Add(string key, string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new System.ArgumentException("Property value required", nameof(value));
            }
            if (key != null && string.IsNullOrWhiteSpace(key))
            {
                throw new System.ArgumentException("Property key required", nameof(key));
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

        public bool TryGetValue(string key, out string value)
        {
            return _impl.TryGetValue(key, out value);
        }

        public string this[string key]
        {
            get => _impl[key];
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));

                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new System.ArgumentException("Property value required", nameof(value));
                }
                if (key != null && string.IsNullOrWhiteSpace(key))
                {
                    throw new System.ArgumentException("Property key required", nameof(key));
                }
                _impl[key] = value;
            }
        }

        public ICollection<string> Keys => _impl.Keys;

        public ICollection<string> Values => _impl.Values;
    }
}
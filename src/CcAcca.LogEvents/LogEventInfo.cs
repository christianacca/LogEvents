using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CcAcca.LogEvents.Internals;

namespace CcAcca.LogEvents
{
    public class LogEventInfo
    {
        private IDictionary<string, string> _properties;
        private IDictionary<string, double> _metrics;

        private const string QualifiedTemplate = "{0}.{1}";


        public LogEventInfo(string name, string prefix = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Event name must be supplied", nameof(name));
            }

            Name = name;
            Prefix = prefix;
            FullName = string.IsNullOrWhiteSpace(prefix) ? name : $"{prefix}.{name}";
        }

        public string FullName { get; }

        public bool HasMetric => _metrics?.Count > 0;
        public bool HasProperty => _properties?.Count > 0;

        public string Name { get; }

        public string Prefix { get; }


        public IDictionary<string, string> Properties => (_properties = _properties ?? new PropertiesDictionary());

        public IDictionary<string, double> Metrics => (_metrics = _metrics ?? new MetricsDictionary());

        private IDictionary<string, T> GetQualifiedCollection<T>(IDictionary<string, T> values)
        {
            if (values == null) return values;

            Func<string, string> identity = k => k;
            Func<string, string> qualified = k => string.Format(QualifiedTemplate, Prefix, k);
            var fieldNameSelector = string.IsNullOrWhiteSpace(Prefix) ? identity : qualified;

            var result = new Dictionary<string, T>(values.Count);
            foreach (var entry in values)
            {
                result[string.Format(fieldNameSelector(entry.Key))] = entry.Value;
            }

            return result;
        }

        public IDictionary<string, double> GetQualifiedMetrics()
        {
            return GetQualifiedCollection(_metrics);
        }

        public IDictionary<string, string> GetQualifiedProperties()
        {
            return GetQualifiedCollection(_properties);
        }

        /// <summary>
        /// Return a string representation of this instance using the
        /// <see cref="DefaultSerializer"/>
        /// </summary>
        public override string ToString()
        {
            return DefaultSerializer.Serialize(this);
        }

        private static ILogEventInfoSerializer _defaultSerializer;

        /// <summary>
        /// The implementation that should be used to return a string
        /// representation of <see cref="LogEventInfo"/>
        /// </summary>
        /// <remarks>
        /// Typically the string produced will be used in output to text based logs
        /// </remarks>
        public static ILogEventInfoSerializer DefaultSerializer
        {
            get => (_defaultSerializer = _defaultSerializer ?? new TextLogEventInfoSerializer());
            set => _defaultSerializer = value;
        }

        public class TextLogEventInfoSerializer : ILogEventInfoSerializer
        {
            private const string EventFieldName = "Event";
            private const string FieldTemplate = "{0}: {1}";

            public string TextLogFieldSeperator { get; set; } = " |";

            public string Serialize(LogEventInfo source)
            {
                if (!source.HasMetric && !source.HasProperty)
                {
                    return string.Format(FieldTemplate, EventFieldName, source.FullName);
                }

                var properties = source.GetQualifiedProperties();
                var metrics = source.GetQualifiedMetrics();
                int elementCount = GetPropertyAndMetricCount(properties, metrics);
                var sb = new StringBuilder(1 + elementCount * 50);
                sb.AppendFormat(FieldTemplate, EventFieldName, source.FullName);
                WriteCollection(properties, sb);
                WriteCollection(metrics, sb);
                return sb.ToString();
            }

            private void WriteCollection<T>(IDictionary<string, T> values, StringBuilder sb)
            {
                if (values == null || !values.Any()) return;

                sb.Append(TextLogFieldSeperator);

                int i = 1;
                foreach (var entry in values)
                {
                    sb.AppendFormat(FieldTemplate, entry.Key, entry.Value);
                    if (i < values.Count)
                    {
                        sb.Append(TextLogFieldSeperator);
                    }

                    i++;
                }
            }

            private static int GetPropertyAndMetricCount(IDictionary<string, string> properties, IDictionary<string, double> metrics)
            {
                int size = 0;
                if (properties != null)
                {
                    size += properties.Count;
                }

                if (metrics != null)
                {
                    size += metrics.Count;
                }

                return size;
            }
        }
    }
}
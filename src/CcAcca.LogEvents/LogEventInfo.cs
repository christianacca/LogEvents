using System;
using System.Collections.Generic;
using System.Linq;
using CcAcca.LogEvents.Internals;

namespace CcAcca.LogEvents
{
    public class LogEventInfo
    {
        private IDictionary<string, string> _properties;
        private IDictionary<string, double> _metrics;
        private DynamicMetricsDictionary _dynamicMetrics;

        public LogEventInfo(string name, string prefix = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Event name must be supplied", nameof(name));
            }

            Name = name;
            Prefix = prefix;
            FieldPrefix = string.IsNullOrWhiteSpace(prefix) ? "" : $"{prefix}.";
            FullName = FieldPrefix + Name;

            Func<string, string> qualified = k => k.StartsWith(FieldPrefix) ? k : FieldPrefix + k;
            FieldNameSelector = string.IsNullOrWhiteSpace(Prefix) ? k => k : qualified;
        }

        public string FullName { get; }

        public bool HasMetric => _metrics?.Count > 0 || _dynamicMetrics?.Count > 0;
        public bool HasProperty => _properties?.Count > 0;

        public string Name { get; }

        public string Prefix { get; }


        public IDictionary<string, string> Properties => (_properties = _properties ?? new PropertiesDictionary());

        public IDictionary<string, double> Metrics => (_metrics = _metrics ?? new MetricsDictionary());

        public IDictionary<string, DynamicMetric> DynamicMetrics => (_dynamicMetrics = _dynamicMetrics ?? new DynamicMetricsDictionary());

        private Func<string, string> FieldNameSelector { get; }

        private string FieldPrefix { get; }

        private IDictionary<string, T> GetQualifiedCollection<T>(IDictionary<string, T> values)
        {
            if (values == null) return values;

            var result = new Dictionary<string, T>(values.Count);
            foreach (var entry in values)
            {
                result[string.Format(FieldNameSelector(entry.Key))] = entry.Value;
            }

            return result;
        }

        public IDictionary<string, double> GetQualifiedMetrics()
        {
            if (!HasMetric) return null;

            // todo: cache the result of GetQualifiedCollection and invalidate when Metrics change
            var metrics = GetQualifiedCollection(_metrics);

            var dynamicMetrics = GetQualifiedCollection(_dynamicMetrics?.Evaluate());

            if (dynamicMetrics == null)
            {
                return metrics;
            }
            if (metrics == null)
            {
                return dynamicMetrics;
            }

            return new[] {metrics, dynamicMetrics}
                .SelectMany(d => d)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public IDictionary<string, string> GetQualifiedProperties()
        {
            // todo: cache the result of GetQualifiedCollection and invalidate when Properties change

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
    }
}
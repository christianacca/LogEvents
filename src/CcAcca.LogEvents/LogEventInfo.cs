using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CcAcca.LogEvents
{
    public class LogEventInfo
    {
        private IDictionary<string, string> _properties;
        private IDictionary<string, double> _metrics;

        public LogEventInfo(string name, string prefix = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Event name must be supplied", nameof(name));
            }

            Name = name;
            Prefix = prefix;
        }

        public string Name { get; }

        public string Prefix { get; }


        public IDictionary<string, string> Properties => (_properties = _properties ?? new Dictionary<string, string>());

        public IDictionary<string, double> Metrics => (_metrics = _metrics ?? new Dictionary<string, double>());

        public override string ToString()
        {
            return DefaultSerializer.Serialize(this);
        }

        private static ILogEventInfoSerializer _defaultSerializer;

        public static ILogEventInfoSerializer DefaultSerializer
        {
            get => (_defaultSerializer = _defaultSerializer ?? new TextLogEventInfoSerializer());
            set => _defaultSerializer = value;
        }

        /// <remarks>
        /// This class is private so as to access fields of <see cref="LogEventInfo"/>
        /// we're doing this to avoid unnecessary object allocations so that
        /// <see cref="LogEventInfo"/> can be used in high performance scenarios
        /// </remarks>
        private class TextLogEventInfoSerializer : ILogEventInfoSerializer
        {
            private const string EventFieldName = "Event";
            private const string FieldSeperator = " |";
            private const string FieldTemplate = "{0}: {1}";
            private const string QualifiedTemplate = "{0}.{1}";

            public string Serialize(LogEventInfo source)
            {
                int elementCount = GetElementCount(source);

                Func<string, string> identity = k => k;
                Func<string, string> qualified = k => string.Format(QualifiedTemplate, source.Prefix, k);
                var fieldNameSelector = string.IsNullOrWhiteSpace(source.Prefix) ? identity : qualified;

                if (elementCount == 0)
                {
                    return string.Format(FieldTemplate, EventFieldName, fieldNameSelector(source.Name));
                }

                var sb = new StringBuilder(1 + elementCount * 50);
                WriteEventName(source, fieldNameSelector, sb);
                WriteProperties(source, fieldNameSelector, sb);
                WriteMetrics(source, fieldNameSelector, sb);
                return sb.ToString();
            }

            private static int GetElementCount(LogEventInfo source)
            {
                int size = 0;
                if (source._properties != null)
                {
                    size += source._properties.Count;
                }
                if (source._metrics != null)
                {
                    size += source._metrics.Count;
                }
                return size;
            }

            private static void WriteEventName(LogEventInfo source, Func<string, string> fieldNameSelector,
                StringBuilder sb)
            {
                sb.AppendFormat(FieldTemplate, EventFieldName, fieldNameSelector(source.Name));
            }

            private static void WriteProperties(LogEventInfo source, Func<string, string> fieldNameSelector,
                StringBuilder sb)
            {
                var values = source._properties
                    ?.Where(x => !string.IsNullOrWhiteSpace(x.Key) && !string.IsNullOrWhiteSpace(x.Value)).ToList();
                WriteCollection(values, fieldNameSelector, sb);
            }

            private static void WriteMetrics(LogEventInfo source, Func<string, string> fieldNameSelector, StringBuilder sb)
            {
                var values = source._metrics
                    ?.Where(x => !string.IsNullOrWhiteSpace(x.Key)).ToList();
                WriteCollection(values, fieldNameSelector, sb);
            }

            private static void WriteCollection<T>(List<KeyValuePair<string, T>> values,
                Func<string, string> fieldNameSelector, StringBuilder sb)
            {
                if (values == null || !values.Any()) return;

                sb.Append(FieldSeperator);
                for (var i = 1; i <= values.Count; i++)
                {
                    var entry = values[i-1];
                    sb.AppendFormat(FieldTemplate, fieldNameSelector(entry.Key), entry.Value);
                    if (i < values.Count)
                    {
                        sb.Append(FieldSeperator);
                    }
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CcAcca.LogEvents.Internals
{
    internal class TextLogEventInfoSerializer : ILogEventInfoSerializer
    {
        private const string EventFieldName = "Event";
        private const string FieldTemplate = "{0}: {1}";

        public string TextLogFieldSeperator { get; set; } = " |";

        public string Serialize(LogEventInfo source)
        {
            if (!source.HasMetric && !source.HasProperty)
            {
                return String.Format(FieldTemplate, EventFieldName, source.FullName);
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
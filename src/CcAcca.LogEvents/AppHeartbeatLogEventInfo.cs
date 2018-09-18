using System;

namespace CcAcca.LogEvents
{
    public class AppHeartbeatLogEventInfo : LogEventInfo
    {
        private const string UptimeMetricKey = "UptimeMinutes";

        public AppHeartbeatLogEventInfo() : base("Heartbeat", LogPrefixes.AppEvent)
        {
            double CalculateElapsedTimeSince(DateTime start)
            {
                return Math.Round((DateTime.Now - start).TotalMinutes, 2);
            }

            DynamicMetrics[UptimeMetricKey] = DynamicMetric.From(CalculateElapsedTimeSince, DateTime.Now);
        }

        public TimeSpan Uptime => DynamicMetrics.ContainsKey(UptimeMetricKey)
            ? TimeSpan.FromMinutes(DynamicMetrics[UptimeMetricKey].Value)
            : TimeSpan.Zero;
    }
}
using System;

namespace CcAcca.LogEvents
{
    public class AppStoppedLogEventInfo : LogEventInfo
    {
        private const string ShutdownMetricKey = "ShutdownMsec";
        private const string TotalRuntimeMetricKey = "TotalRuntimeMsec";

        public AppStoppedLogEventInfo(DateTime? signalTime = null, DateTime? appStarted = null) : base("Stopped", LogPrefixes.AppEvent)
        {
            if (signalTime.HasValue)
            {
                ShutdownDuration = DateTime.Now - signalTime.Value;
            }
            if (appStarted.HasValue)
            {
                TotalRuntime = DateTime.Now - appStarted.Value;
            }
        }

        public TimeSpan ShutdownDuration
        {
            get => Metrics.ContainsKey(ShutdownMetricKey)
                ? TimeSpan.FromMilliseconds(Metrics[ShutdownMetricKey])
                : TimeSpan.Zero;
            set => Metrics[ShutdownMetricKey] = Math.Round(value.TotalMilliseconds, 0);
        }

        public TimeSpan TotalRuntime
        {
            get => Metrics.ContainsKey(TotalRuntimeMetricKey)
                ? TimeSpan.FromMilliseconds(Metrics[TotalRuntimeMetricKey])
                : TimeSpan.Zero;
            set => Metrics[TotalRuntimeMetricKey] = Math.Round(value.TotalMilliseconds, 0);
        }
    }
}
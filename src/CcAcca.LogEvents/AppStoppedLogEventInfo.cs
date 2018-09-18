using System;

namespace CcAcca.LogEvents
{
    public class AppStoppedLogEventInfo : LogEventInfo
    {
        private const string ShutdownMetricKey = "ShutdownMsec";
        private const string RuntimeMetricKey = "RuntimeMinutes";

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
            get => Metrics.ContainsKey(RuntimeMetricKey)
                ? TimeSpan.FromMinutes(Metrics[RuntimeMetricKey])
                : TimeSpan.Zero;
            set => Metrics[RuntimeMetricKey] = Math.Round(value.TotalMinutes, 2);
        }
    }
}
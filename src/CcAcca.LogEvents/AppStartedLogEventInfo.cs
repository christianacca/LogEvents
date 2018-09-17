using System;

namespace CcAcca.LogEvents
{
    public class AppStartedLogEventInfo: LogEventInfo
    {
        private const string AppStartupMetricKey = "StartupMsec";

        public AppStartedLogEventInfo(DateTime? startupTime = null) : base("Started", LogPrefixes.AppEvent)
        {
            if (startupTime.HasValue)
            {
                Duration = DateTime.Now - startupTime.Value;
            }
        }

        public TimeSpan Duration
        {
            get => Metrics.ContainsKey(AppStartupMetricKey)
                ? TimeSpan.FromMilliseconds(Metrics[AppStartupMetricKey])
                : TimeSpan.Zero;
            set => Metrics[AppStartupMetricKey] = Math.Round(value.TotalMilliseconds, 0);
        }
    }
}
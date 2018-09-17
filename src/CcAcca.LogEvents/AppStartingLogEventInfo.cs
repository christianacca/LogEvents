namespace CcAcca.LogEvents
{
    public class AppStartingLogEventInfo: LogEventInfo
    {
        public AppStartingLogEventInfo() : base("Starting", LogPrefixes.AppEvent)
        {
        }
    }
}
namespace CcAcca.LogEvents
{
    public class AppStoppingLogEventInfo : LogEventInfo
    {
        public AppStoppingLogEventInfo() : base("Stopping", LogPrefixes.AppEvent)
        {
        }
    }
}
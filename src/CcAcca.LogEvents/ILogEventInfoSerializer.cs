namespace CcAcca.LogEvents
{
    public interface ILogEventInfoSerializer
    {
        string Serialize(LogEventInfo source);
    }
}
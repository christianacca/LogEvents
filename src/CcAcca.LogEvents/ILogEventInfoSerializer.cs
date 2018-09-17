namespace CcAcca.LogEvents
{
    public interface ILogEventInfoSerializer
    {
        string Serialize(LogEventInfo source);

        /// <summary>
        /// The seperator to use when serializing <see cref="LogEventInfo"/> fields for output to a text log
        /// </summary>
        string TextLogFieldSeperator { get; set; }
    }
}
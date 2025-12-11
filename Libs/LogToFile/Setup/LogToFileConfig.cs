namespace LogToFile.Setup;

public class LogToFileConfig
{
    //public int EventId { get; set; }
    /// <summary>
    /// The EventIds that will be logged. DO not set to Log all events that match the filter or minimum level;
    /// </summary>
    public HashSet<int> EventIds { get; set; } = [];


}//Cls


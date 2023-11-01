using Application.Domain;

namespace Application;
public class AppSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string PathFile { get; set; } = string.Empty;
    public WorkDay WorkDay { get; set; } = new WorkDay();
    public List<TimeOnly> BaseHours {get; set;} = new List<TimeOnly>();
}


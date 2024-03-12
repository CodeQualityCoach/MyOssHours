namespace MyOssHours.Backend.Presentation.Requests;

public class CreateProjectHourCommand
{
    public Guid Project { get; set; }
    public Guid WorkItem { get; set; }
    public DateOnly Date { get; set; }
    public int DurationInMinutes { get; set; }
    public Guid User { get; set; }
    public string? Description { get; set; }
}
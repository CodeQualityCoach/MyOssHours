namespace MyOssHours.Backend.Presentation.Requests;

public class CreateWorkItemCommand
{
    public Guid Project { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
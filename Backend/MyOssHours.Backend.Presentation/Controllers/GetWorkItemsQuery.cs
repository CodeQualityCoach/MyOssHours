namespace MyOssHours.Backend.Presentation.Controllers;

public class GetWorkItemsQuery
{
    public required int Offset { get; set; }
    public required int Size { get; set; }
    public required Guid Project { get; set; }
}
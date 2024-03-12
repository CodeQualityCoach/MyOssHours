namespace MyOssHours.Backend.Presentation.Requests;

public class GetProjectsQuery
{
    public int Offset { get; set; } = 0;
    public int Size { get; set; } = 20;
    public string? NameLike { get; set; }
}
namespace DataTransfer.Objects;

public class TaskDto
{
    // make workable Id property
    public int Id { get; set; }
    // nullable or not?
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? Deadline { get; set; }
    public bool IsCompleted { get; set; }
}

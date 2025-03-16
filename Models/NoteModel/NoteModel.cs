using System.ComponentModel.DataAnnotations;
public class Note
{
    [Key]
    public Guid noteId { get; set; }
    public Guid id { get; set; }
    public string? title { get; set; }
    public string? description { get; set; }
    public bool? done { get; set; }
    public DateTime createdAt { get; set; } = DateTime.UtcNow;
    public bool isImportant { get; set; } = false;
}

namespace apief
{
    public class NoteDto
    {
        public Guid noteId { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public bool? done { get; set; }
        public bool isImportant { get; set; } = false;
    }
}
namespace apief
{
    public class NoteCreateDto
    {
        public string? title { get; set; }
        public string? description { get; set; }
        public bool? done { get; set; }
        public bool isImportant { get; set; } = false;
    }
}
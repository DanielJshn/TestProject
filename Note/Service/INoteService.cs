namespace apief
{
    public interface INoteService
    {
        Task<NoteDto> CreateNoteAsync(NoteDto noteDto, Guid userId);
        Task<List<NoteDto>> GetNotesAsync(Guid userId);
        Task<NoteDto> UpdateNoteAsync(Guid noteId, NoteDto noteDto, Guid userId);
        Task DeleteNoteAsync(Guid noteId, Guid userId);
    }
}
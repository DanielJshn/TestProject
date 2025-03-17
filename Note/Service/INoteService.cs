namespace apief
{
    public interface INoteService
    {
        Task<NoteDto> CreateNoteAsync(NoteCreateDto noteDto, Guid userId);
        Task<List<NoteDto>> GetNotesAsync(Guid userId, bool isImportant);
        Task<NoteDto> UpdateNoteAsync(Guid noteId, NoteCreateDto noteDto, Guid userId);
        Task DeleteNoteAsync(Guid noteId, Guid userId);
    }
}
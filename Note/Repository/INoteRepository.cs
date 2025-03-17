namespace apief
{
    public interface INoteRepository
    {
        Task AddAsync(Note note);
        Task<IEnumerable<Note>> GetNotesAsync(Guid userId, bool isImportant);
        Task<Note> GetNoteByNoteId(Guid Id);
        Task UpdateAsync(Note note);
        Task DeleteNoteAsync(Guid noteId);
    }
}
using Microsoft.EntityFrameworkCore;

namespace apief
{
    public class NoteRepository : INoteRepository
    {
        private readonly DataContextEF _dataContext;

        public NoteRepository(DataContextEF dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddAsync(Note note)
        {
            await _dataContext.Notes.AddAsync(note);
            await _dataContext.SaveChangesAsync();
        }


        public async Task<IEnumerable<Note>> GetNotesAsync(Guid userId, bool isImportant)
        {
            return await _dataContext.Notes
                .Where(t => t.id == userId && (!isImportant || t.isImportant))
                .ToListAsync();
        }



        public async Task<Note> GetNoteByNoteId(Guid noteId)
        {
            var result = await _dataContext.Notes.FirstOrDefaultAsync(t => t.noteId == noteId);

            if (result == null)
            {
                throw new Exception("Note is not found");
            }
            return result;
        }


        public async Task UpdateAsync(Note note)
        {
            _dataContext.Notes.Update(note);

            await _dataContext.SaveChangesAsync();
        }


        public async Task DeleteNoteAsync(Guid noteId)
        {
            var note = await _dataContext.Notes
                .FirstOrDefaultAsync(p => p.noteId == noteId);

            if (note != null)
            {
                _dataContext.Notes.Remove(note);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}

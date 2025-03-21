using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace apief
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public NoteService(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }


        public async Task<NoteDto> CreateNoteAsync(NoteCreateDto noteDto, Guid userId)
        {

            if (string.IsNullOrWhiteSpace(noteDto.title))
            {
                throw new ArgumentException("Title is required");
            }

            var noteModel = _mapper.Map<Note>(noteDto);
            noteModel.noteId = Guid.NewGuid();
            noteModel.id = userId;

            await _noteRepository.AddAsync(noteModel);

            return _mapper.Map<NoteDto>(noteModel);
        }


        public async Task<List<NoteDto>> GetNotesAsync(Guid userId, bool isImportant)
        {
            var notes = await _noteRepository.GetNotesAsync(userId, isImportant);

            if (notes == null || !notes.Any())
            {
                return new List<NoteDto>();
            }

            var noteDto = _mapper.Map<List<NoteDto>>(notes);

            return noteDto;
        }


        public async Task<NoteDto> UpdateNoteAsync(Guid noteId, NoteCreateDto noteDto, Guid userId)
        {

            var note = await _noteRepository.GetNoteByNoteId(noteId);
            if (note == null)
            {
                throw new KeyNotFoundException($"Note with ID {noteId} not found");
            }

            if (string.IsNullOrWhiteSpace(noteDto.title))
            {
                throw new ArgumentException("Title is required");
            }

            note.title = noteDto.title;
            note.description = noteDto.description;
            note.done = noteDto.done;
            note.isImportant = noteDto.isImportant;
            note.createdAt = DateTime.UtcNow;

            await _noteRepository.UpdateAsync(note);

            var updatedNotes = await _noteRepository.GetNoteByNoteId(noteId);

            _mapper.Map<NoteDto>(updatedNotes);

            return _mapper.Map<NoteDto>(updatedNotes);
        }


        public async Task DeleteNoteAsync(Guid noteId, Guid userId)
        {

            var existingNote = await _noteRepository.GetNoteByNoteId(noteId);

            if (existingNote == null)
            {
                throw new Exception("Note not found");
            }
            await _noteRepository.DeleteNoteAsync(noteId);
        }
    }
}

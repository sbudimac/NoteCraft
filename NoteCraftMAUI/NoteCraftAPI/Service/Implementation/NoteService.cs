using AutoMapper;
using NoteCraftAPI.Repository;
using NoteCraftModels;

namespace NoteCraftAPI.Service.Implementation
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository noteRepository;
        private readonly IMapper mapper;

        public NoteService(INoteRepository noteRepository, IMapper mapper)
        {
            this.noteRepository = noteRepository;
            this.mapper = mapper;
        }

        public Note GetById(string id)
        {
            return noteRepository.GetById(id);
        }

        public List<Note> GetByTitle(string title)
        {
            return noteRepository.GetByTitle(title);
        }

        public List<Note> GetByUser(string userId)
        {
            return noteRepository.GetByUser(userId);
        }

        public Note CreateNote(NoteDto newNote)
        {
            Note note = mapper.Map<Note>(newNote);
            return noteRepository.CreateNote(note);
        }

        public Note UpdateNote(string userId, string noteId, NoteDto updatedNote)
        {
            Note note = mapper.Map<Note>(updatedNote);
            note.Id = noteId;
            return noteRepository.UpdateNote(userId, noteId, note);
        }

        public void DeleteNote(string userId, string id)
        {
            noteRepository.DeleteNote(userId, id);
        }
    }
}

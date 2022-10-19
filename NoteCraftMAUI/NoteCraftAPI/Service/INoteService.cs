using NoteCraftModels;

namespace NoteCraftAPI.Service
{
    public interface INoteService
    {
        Note GetById(string id);
        List<Note> GetByTitle(string title);
        List<Note> GetByUser(string userId);
        Note CreateNote(NoteDto note);
        Note UpdateNote(string userId, string noteId, NoteDto note);
        void DeleteNote(string userId, string id);
    }
}

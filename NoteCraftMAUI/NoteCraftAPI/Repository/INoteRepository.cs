using NoteCraftModels;

namespace NoteCraftAPI.Repository
{
    public interface INoteRepository
    {
        Note GetById(string id);
        List<Note> GetByTitle(string title);
        List<Note> GetByUser(string userId);
        Note CreateNote(Note note);
        Note UpdateNote(string userId, string noteId, Note note);
        void DeleteNote(string userId, string id);
        List<Note> GetShared(string userId);
        Note ShareNote(string username, string noteId);
    }
}

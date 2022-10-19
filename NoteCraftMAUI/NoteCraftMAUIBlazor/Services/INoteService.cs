using NoteCraftModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteCraftMAUIBlazor.Services
{
    public interface INoteService
    {
        Task<Note> GetById(string id);
        Task<List<Note>> GetByTitle(string title);
        Task<List<Note>> GetByUser(string userId);
        Task<Note> CreateNote(NoteDto newNote);
        Task<Note> UpdateNote(string userId, string noteId, NoteDto updatedNote);
        Task DeleteNote(string userId, string noteId);
    }
}

using Microsoft.AspNetCore.Components;
using NoteCraftModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteCraftMAUIBlazor.Services.Implementation
{
    public class NoteService : INoteService
    {
        private readonly IHttpService httpService;
        private readonly NavigationManager navigationManager;

        public NoteService(IHttpService httpService, NavigationManager navigationManager)
        {
            this.httpService = httpService;
            this.navigationManager = navigationManager;
        }

        public async Task<Note> GetById(string id)
        {
            var note = await httpService.Get<Note>($"api/note/{id}");
            return note;
        }

        public async Task<List<Note>> GetByTitle(string title)
        {
            var notes = await httpService.Get<List<Note>>($"api/note/title/{title}");
            return notes;
        }

        public async Task<List<Note>> GetByUser(string userId)
        {
            var notes = await httpService.Get<List<Note>>($"api/note/user/{userId}");
            return notes;
        }

        public async Task<Note> CreateNote(NoteDto newNote)
        {
            var createdNote = await httpService.Post<Note>("api/note/create", newNote);
            return createdNote;
        }

        public async Task<Note> UpdateNote(string userId, string noteId, NoteDto updatedNote)
        {
            var newNote = await httpService.Put<Note>($"api/note/{userId}/{noteId}", updatedNote);
            return newNote;
        }

        public async Task DeleteNote(string userId, string noteId)
        {
            await httpService.Delete($"api/note/{userId}/{noteId}");
        }
    }
}

﻿using MongoDB.Driver;
using NoteCraftAPI.Settings;
using NoteCraftModels;

namespace NoteCraftAPI.Repository.Implementation
{
    public class NoteRepository : INoteRepository
    {
        private readonly IMongoCollection<Note> notes;
        private readonly IUserRepository userRepository;

        public NoteRepository(INotecraftDatabaseSettings settings, IMongoClient mongoClient, IUserRepository userRepository)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            notes = database.GetCollection<Note>(settings.NotesCollectionName);
            this.userRepository = userRepository;
        }

        public Note GetById(string id)
        {
            return notes.Find(note => note.Id == id).FirstOrDefault();
        }

        public List<Note> GetByTitle(string title)
        {
            return notes.Find(note => note.Title == title).ToList();
        }

        public List<Note> GetByUser(string userId)
        {
            return notes.Find(note => note.OwnerId == userId).ToList();
        }

        public Note CreateNote(Note note)
        {
            notes.InsertOne(note);
            AddNoteToUser(note.OwnerId, note);

            return note;
        }

        public Note UpdateNote(string userId, string noteId, Note note)
        {
            UpdateUserNote(userId, note);
            notes.ReplaceOne(note => note.Id == noteId, note);

            return note;
        }

        public void DeleteNote(string userId, string id)
        {
            RemoveNoteFromUser(userId, id);
            notes.DeleteOne(note => note.Id == id);
        }

        public void AddNoteToUser(string userId, Note note)
        {
            User user = userRepository.GetById(userId);
            user.UserNotes.Add(note);
            userRepository.UpdateUser(userId, user);
        }

        public void UpdateUserNote(string userId, Note note)
        {
            User user = userRepository.GetById(userId);
            for (int i = 0; i < user.UserNotes.Count; i++)
            {
                if (user.UserNotes[i].Id == note.Id)
                {
                    user.UserNotes[i] = note;
                    break;
                }
            }
            userRepository.UpdateUser(userId, user);
        }

        public void RemoveNoteFromUser(string userId, string noteId)
        {
            User user = userRepository.GetById(userId);
            foreach (var note in user.UserNotes)
            {
                if (note.Id == noteId)
                {
                    user.UserNotes.Remove(note);
                    break;
                }
            }
            userRepository.UpdateUser(userId, user);
        }
    }
}

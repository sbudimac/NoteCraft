using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteCraftModels
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username must be provided")]
        [MinLength(2)]
        [BsonElement("username")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email must be provided")]
        [EmailAddress]
        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("password_hash")]
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

        [BsonElement("password_salt")]
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

        [BsonElement("notes")]
        public List<Note> UserNotes { get; set; } = new List<Note>();

        [BsonElement("shared_notes")]
        public Dictionary<string, Note> SharedNotes { get; set; } = new Dictionary<string, Note>();
    }
}

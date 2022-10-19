using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteCraftModels
{
    [BsonIgnoreExtraElements]
    public class Note
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("title")]
        [Required]
        [MinLength(2)]
        public string Title { get; set; } = string.Empty;

        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("content")]
        [Required]
        public string Content { get; set; } = string.Empty;

        [BsonElement("note_type")]
        public NoteType NoteType { get; set; } = NoteType.Text;

        [BsonElement("note_importance")]
        [Required]
        public NoteImportance NoteImportance { get; set; } = NoteImportance.Medium;

        [BsonElement("comments")]
        public List<Comment> Comments { get; set; } = new List<Comment>();

        [BsonElement("owner_id")]
        [Required]
        public string OwnerId { get; set; } = string.Empty;
    }
}

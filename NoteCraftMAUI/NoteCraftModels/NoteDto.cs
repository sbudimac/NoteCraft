using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteCraftModels
{
    public class NoteDto
    {
        [Required(ErrorMessage = "Note title must be provided")]
        [MinLength(2)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(25)]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Note content must be provided")]
        public string Content { get; set; } = string.Empty;
        [Required]
        public NoteType NoteType { get; set; } = NoteType.Text;
        [Required]
        public NoteImportance NoteImportance { get; set; } = NoteImportance.Medium;
        public List<string> Sharedusers { get; set; } = new List<string>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        [Required]
        public string OwnerId { get; set; } = string.Empty;

        public NoteDto()
        {

        }

        public NoteDto(string title, string content, NoteType noteType, NoteImportance noteImportance, List<string> sharedUsers, List<Comment> comments, string ownerId)
        {
            Title = title;
            Content = content;
            NoteType = noteType;
            NoteImportance = noteImportance;
            Sharedusers = sharedUsers;
            Comments = comments;
            OwnerId = ownerId;
        }

        public NoteDto(string title, string description, string content, NoteType noteType, NoteImportance noteImportance, List<string> sharedUsers, List<Comment> comments, string ownerId)
        {
            Title = title;
            Description = description;
            Content = content;
            NoteType = noteType;
            NoteImportance = noteImportance;
            Sharedusers = sharedUsers;
            Comments = comments;
            OwnerId = ownerId;
        }
    }
}

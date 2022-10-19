using Microsoft.AspNetCore.Mvc;
using NoteCraftAPI.Service;
using NoteCraftModels;

namespace NoteCraftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService noteService;

        public NoteController(INoteService noteService)
        {
            this.noteService = noteService;
        }

        [HttpGet("{id}")]
        public ActionResult<Note> GetById(string id)
        {
            var note = noteService.GetById(id);
            if (note == null)
            {
                return NotFound($"Note with id = {id} not found.");
            }

            return Ok(note);
        }

        [HttpGet("title/{title}")]
        public ActionResult<List<Note>> GetByTitle(string title)
        {
            var note = noteService.GetByTitle(title);
            if (note == null)
            {
                return NotFound($"Note with title = {title} not found.");
            }

            return Ok(note);
        }

        [HttpGet("user/{userId}")]
        public ActionResult<List<Note>> GetByUser(string userId)
        {
            Console.WriteLine("GET");
            var note = noteService.GetByUser(userId);
            if (note == null)
            {
                return NotFound($"No notes for user with id = {userId} found.");
            }

            return Ok(note);
        }

        [HttpPost("create")]
        public ActionResult<Note> CreateNote(NoteDto newNote)
        {
            Console.WriteLine("POST");
            try
            {
                if (newNote == null)
                {
                    return BadRequest();
                }

                var note = noteService.CreateNote(newNote);
                return Ok(note);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured while trying to create a new note.");
            }
        }

        [HttpPut("{userId}/{noteId}")]
        public ActionResult<Note> UpdateNote(string userId, string noteId, NoteDto updatedNote)
        {
            try
            {
                if (string.IsNullOrEmpty(noteId) || updatedNote == null)
                {
                    return BadRequest();
                }

                var note = noteService.UpdateNote(userId, noteId, updatedNote);
                return Ok(note);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured while trying to update a note.");
            }
        }

        [HttpDelete("{userId}/{noteId}")]
        public ActionResult DeleteNote(string userId, string noteId)
        {
            var note = noteService.GetById(noteId);
            if (note == null)
            {
                return NotFound($"Note with Id = {noteId} not found.");
            }

            noteService.DeleteNote(userId, noteId);
            return Ok($"Note with Id = {noteId} deleted.");
        }
    }
}

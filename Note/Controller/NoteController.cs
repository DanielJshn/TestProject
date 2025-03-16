using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apief
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly IIdentityUser _identityUser;
        public NoteController(INoteService noteService, IIdentityUser identityUser)
        {
            _noteService = noteService;
            _identityUser = identityUser;
        }
        

        [HttpPost]
        public async Task<IActionResult> PostNote(NoteCreateDto noteDto)
        {
            try
            {
                var identity = await _identityUser.GetUserByTokenAsync(User);

                if (identity.id == Guid.Empty)
                {
                    return BadRequest(new ApiResponse(success: false, message: "Invalid user ID"));
                }

                var createdNote = await _noteService.CreateNoteAsync(noteDto, identity.id);
                return Ok(new ApiResponse(success: true, data: createdNote));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(success: false, message: ex.Message));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetNote()
        {
            try
            {
                var identity = await _identityUser.GetUserByTokenAsync(User);
                var response = await _noteService.GetNotesAsync(identity.id);
                return Ok(new ApiResponse(success: true, data: response));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(success: false, message: ex.Message));
            }
        }

        [HttpPut("noteId")] 
        public async Task<IActionResult> PutNote(Guid noteId, NoteCreateDto note)
        {
            try
            {
                var identity = await _identityUser.GetUserByTokenAsync(User);
                var updateNote = await _noteService.UpdateNoteAsync(noteId, note, identity.id);
                return Ok(new ApiResponse(success: true, data: updateNote));

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(success: false, message: ex.Message));
            }
        }

        [HttpDelete("noteId")] 
        public async Task<IActionResult> DeleteNote(Guid noteId)
        {
            try
            {
                var identity = await _identityUser.GetUserByTokenAsync(User);
                await _noteService.DeleteNoteAsync(noteId, identity.id);
                return Ok(new ApiResponse(success: true, data: Ok()));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(success: false, message: ex.Message));
            }
        }
    }
}
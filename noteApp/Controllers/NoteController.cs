using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using noteApp.Dtos;
using noteApp.Models;

namespace noteApp.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NoteController : ControllerBase
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public NoteController(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private Note? GetNoteById(Guid id)
    {
        return _context.Notes?.Find(id);
    }

    [HttpGet]
    public IActionResult GetNotes()
    {
        string? userId = User.FindFirst("userId")?.Value;
        if (userId == null)
        {
            return NotFound("UserId is required");
        }

        List<Note?>? notes = _context.Notes?.ToList();


        List<NoteForReadDto> noteDtos = _mapper.Map<List<NoteForReadDto>>(notes);
        return Ok(noteDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetNote(Guid id)
    {
        Note? note = GetNoteById(id);
        NoteForReadDto noteDto = _mapper.Map<NoteForReadDto>(note);

        return noteDto != null ? Ok(noteDto) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateNote(NoteForCreateDto noteForCreate)
    {
        string? userId = User.FindFirst("userId")?.Value;
        User? user = _context.Users.Find(Guid.Parse(userId));

        if (user == null)
        {
            return NotFound("User is required");
        }

        Note noteDb = _mapper.Map<Note>(noteForCreate);
        noteDb.User = user;

        _context.Notes?.Add(noteDb);
        await _context.SaveChangesAsync();

        return Ok(new { id = noteDb.Id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNote(Guid id, NoteForUpdateDto noteToUpdate)
    {
        if (id != noteToUpdate.Id)
        {
            return BadRequest();
        }
        string? userId = User.FindFirst("userId")?.Value;


        Note note = _mapper.Map<Note>(noteToUpdate);
        note.UserId = Guid.Parse(userId);

        _context.Notes.Update(note);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(Guid id)
    {
        Note? note = GetNoteById(id);
        if (note == null)
        {
            return NotFound();
        }

        _context.Notes?.Remove(note);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
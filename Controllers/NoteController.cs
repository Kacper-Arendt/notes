using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using note.Dtos;
using note.Models;

namespace note.Controllers;

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

    private Note? GetNoteById(int id)
    {
        return _context.Notes?.Find(id);
    }
    
    [HttpGet]
    public IActionResult GetNotes()
    {
        List<Note?>? notes = _context.Notes?.ToList();
        
        List<NoteForReadDto> noteDtos = _mapper.Map<List<NoteForReadDto>>(notes);

        return Ok(noteDtos);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetNote(int id)
    {
        Note? note = GetNoteById(id);
        NoteForReadDto noteDto = _mapper.Map<NoteForReadDto>(note);
        return noteDto != null ? Ok(noteDto) : NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateNote(NoteForCreateDto noteForCreate)
    {
        Note noteDb = _mapper.Map<Note>(noteForCreate);
        
        _context.Notes?.Add(noteDb);
        await _context.SaveChangesAsync();

        return Ok(new {id= noteDb.Id});
    }   
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNote(int id, NoteForUpdateDto noteToUpdate)
    {
        if (id != noteToUpdate.Id)
        {
            return BadRequest();
        }
        
        Note note = _mapper.Map<Note>(noteToUpdate);
        
        _context.Entry(note).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (GetNoteById(id) == null)
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }  
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id)
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
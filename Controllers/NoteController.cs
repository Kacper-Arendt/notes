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

    public NoteController(DbContextOptions options)
    {
        _context = new DatabaseContext(options);
    }

    private Note? GetNoteById(int id)
    {
        return _context.Notes?.Find(id);
    }
    
    [HttpGet]
    public IEnumerable<Note>? GetNotes()
    {
        List<Note?>? notes = _context.Notes?.ToList();
        return notes;
    }
    
    [HttpGet("{id}")]
    public IActionResult GetNote(int id)
    {
        Note? note = GetNoteById(id);
        return note != null ? Ok(note) : NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateNote(NoteForCreateDto noteForCreate)
    {
        MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<NoteForCreateDto, Note>());
        Mapper mapper = new Mapper(config);
        Note noteDb = mapper.Map<Note>(noteForCreate);
        
        _context.Notes?.Add(noteDb);
        await _context.SaveChangesAsync();

        return Ok(new {id= noteDb.Id});
    }   
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNote(int id, Note noteToUpdate)
    {
        if (id != noteToUpdate.Id)
        {
            return BadRequest();
        }
        
        _context.Entry(noteToUpdate).State = EntityState.Modified;
        
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
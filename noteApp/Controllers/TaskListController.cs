using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using noteApp.Dtos;
using noteApp.Models;

namespace noteApp.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]")]
public class TaskListController : Controller
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public TaskListController(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetTaskLists()
    {
        var taskLists = await _context.TaskList.ToListAsync();
        var noteDtos = _mapper.Map<List<TaskList>>(taskLists);

        return Ok(noteDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskList(Guid id)
    {
        var taskList = await _context.TaskList.FindAsync(id);

        if (taskList == null) return NotFound();
        var taskListDto = _mapper.Map<TaskListForRead>(taskList);

        return Ok(taskListDto);
    }

    [HttpPost]
    public async Task<IActionResult> AddList([FromBody] TaskListForCreate taskListForCreate)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        TaskList list = _mapper.Map<TaskList>(taskListForCreate);

        _context.TaskList.Add(list);
        await _context.SaveChangesAsync();

        return Ok(list);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTaskList(Guid id, TaskListForUpdate taskListDto)
    {
        if (!ModelState.IsValid || id != taskListDto.ListId)
        {
            return BadRequest(ModelState);
        }

        var taskListToUpdate = await _context
            .TaskList
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.ListId == id);

        if (taskListToUpdate == null) return NotFound();

        var taskToUpdateDto = _mapper.Map<TaskList>(taskListDto);

        _context.TaskList.Update(taskToUpdateDto);

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
    public async Task<IActionResult> DeleteTaskList(Guid id)
    {
        var taskList = await _context.TaskList.FindAsync(id);
        if (taskList == null) return NotFound();

        _context.TaskList.Remove(taskList);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
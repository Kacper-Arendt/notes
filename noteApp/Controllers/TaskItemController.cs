using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using noteApp.Dtos;
using noteApp.Enums;
using noteApp.Models;

namespace noteApp.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]")]
public class TaskItemController : Controller
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public TaskItemController(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    [HttpPost]
    public async Task<IActionResult> AddTask([FromBody]TaskForCreate taskForCreate)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var taskList = await _context
            .TaskList
            .Include(taskList => taskList.TasksList)
            .FirstOrDefaultAsync(i => i.ListId == taskForCreate.TaskListId);

        if (taskList == null) return NotFound("parent not found");

        TaskItem task = _mapper.Map<TaskItem>(taskForCreate);
        task.Status = Status.InProgress;

        taskList.TasksList.Add(task);

        _context
            .TaskItem
            .Add(task);
        
        await _context.SaveChangesAsync();
        var taskForRead = _mapper.Map<TaskForRead>(task);

        return Ok(taskForRead);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTaskItem(Guid id, TaskForUpdate taskDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var task = await _context
            .TaskItem
            .FirstOrDefaultAsync(i => i.TaskId == id);

        if (task == null) return NotFound();

        task.Name = taskDto.Name;
        task.Status = taskDto.Status;
        task.Priority = taskDto.Priority;
        task.DueDate = taskDto.DueDate;
        
        
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaskList(Guid id)
    {
        var task = await _context
            .TaskItem
            .FindAsync(id);
        
        if (task == null) return NotFound();

        _context.TaskItem.Remove(task);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
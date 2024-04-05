using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using noteApp.Models;

namespace note.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public UserController(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetUser()
    {
        string? userId = User.FindFirst("userId")?.Value;
        if (userId == null)
        {
            return NotFound("UserId is required");
        }

        User user = _context.Users
            .Include(e => e.Notes)
            .FirstOrDefault(i => i.Id == Guid.Parse(userId));
        return Ok(user);
    }
}
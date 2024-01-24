using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using note.Dtos;
using note.helpers;
using note.Models;

namespace note.Controllers;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    private readonly DatabaseContext _context;
    
    private readonly IMapper _mapper;
    
    private readonly AuthHelper _authHelper;

    public AuthController(DatabaseContext context, IMapper mapper, IConfiguration config)
    {
        _context = context;
        _mapper = mapper;
        _authHelper = new AuthHelper(config);
    }
    
    
    [AllowAnonymous]
    [HttpPost("RegisterUser")]
    public async Task<IActionResult> Register(UserAuthForCreateDto userForCreate)
    {
        try
        {
            if (userForCreate.Password != userForCreate.PasswordConfirm)
            {
                return BadRequest("Password and password confirmation do not match.");
            }

            bool userExist = await _context.UserAuthentications.FindAsync(userForCreate.Email) != null;
            
            if (userExist)
            {
                return BadRequest("User with given email exists");
            }

            PasswordHelper passwordHelper = _authHelper.HashPassword(userForCreate.Password);

            UserAuthentication userForRegister = new UserAuthentication(userForCreate.Email)
            {
                Salt = passwordHelper.PasswordSalt,
                PasswordHash = passwordHelper.PasswordHash
            };

            _context.UserAuthentications.Add(userForRegister);
            int created = await _context.SaveChangesAsync();
        
            return created > 0 ?  Ok() : BadRequest();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }  
}
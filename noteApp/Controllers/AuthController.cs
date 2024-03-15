using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using noteApp.Dtos;
using noteApp.helpers;
using noteApp.Models;

namespace note.Controllers;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
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
    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserAuthForCreateDto userForCreate)
    {
        try
        {
            if (userForCreate.Password != userForCreate.PasswordConfirm)
            {
                return BadRequest("Password and password confirmation do not match.");
            }

            bool userExist =  _context.Users.FirstOrDefault(e => e.Email == userForCreate.Email) != null;

            if (userExist)
            {
                return BadRequest("User with given email exists");
            }

            PasswordHelper passwordHelper = _authHelper.HashPassword(userForCreate.Password);
            User user = new User(userForCreate.Email);

            UserAuthentication userForRegister = new UserAuthentication(passwordHelper.PasswordHash, passwordHelper.PasswordSalt);
            userForRegister.User = user;
            user.UserAuthentication = userForRegister;
            
            _context.Users.Add(user);;
            _context.UserAuthentications.Add(userForRegister);
            
            int created = await _context.SaveChangesAsync();

            return created > 0 ? Ok() : BadRequest();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserAuthForLoginDto userForLogin)
    {
        try
        {
            User? user = _context.Users.FirstOrDefault(e => e.Email == userForLogin.Email);

            if (user == null)
            {
                return NotFound("Incorrect credentials");
            }
            
            UserAuthentication? authUser =  _context.UserAuthentications.FirstOrDefault(u => u.UserId ==user.Id);
            if (authUser == null)
            {
                return NotFound("Incorrect credentials");
            }
            
            byte[] passwordHash = _authHelper.GetPasswordHash(userForLogin.Password, authUser.Salt);

            for (int index = 0; index < passwordHash.Length; index++)
            {
                if (passwordHash[index] != authUser.PasswordHash[index]){
                    return StatusCode(401, "Incorrect credentials");
                }
            }

            return Ok(new Dictionary<string, string> {
                {"token", _authHelper.CreateToken(user.Id)}
            });

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}
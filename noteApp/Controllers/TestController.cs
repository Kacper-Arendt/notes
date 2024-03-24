using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace noteApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;

    public TestController( IMapper mapper, IConfiguration config)
    {
        _mapper = mapper;
        _config = config;
    }

    [HttpGet("test")]
    public string test()
    {    
        return "App and working2" + _config.GetConnectionString("DefaultConnection");
    }
    
    [HttpGet("test2")]
    public string GetNotes()
    {    
        return "App and unning1";
    }
}
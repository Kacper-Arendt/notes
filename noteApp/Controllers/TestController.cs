using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace noteApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly IMapper _mapper;

    public TestController( IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet("test")]
    public string test()
    {    
        return "App and unning22";
    }
    
    [HttpGet("test2")]
    public string GetNotes()
    {    
        return "App and unning1";
    }
}
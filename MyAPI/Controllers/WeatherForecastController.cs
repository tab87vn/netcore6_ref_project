namespace tab.TestDotNet.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tab.TestDotNet.API.Models;
using tab.TestDotNet.API.Repositories;
using tab.TestDotNet.Services.Contracts;

[ApiController]
[Route("api/test")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    private readonly ILoggerManager _myLogger;

    private readonly IUSerRepository _userRepo;

    public WeatherForecastController(
        ILoggerManager myLogger,
        ILogger<WeatherForecastController> logger,
        IUSerRepository userRepo)
    {
        _myLogger = myLogger;
        _logger = logger;
        _userRepo = userRepo;
    }

    [HttpGet ]
    public IActionResult DoSomething(string? lol = null)
    {
        _myLogger.LogInfo("Here is info message from our values controller.");
        _myLogger.LogDebug("Here is debug message from our values controller.");
        _myLogger.LogWarn("Here is warn message from our values controller.");
        _myLogger.LogError("Here is an error message from our values controller.");

        return Ok(new string[] { "value1", "value2", lol });
    }

    // [HttpGet(Name = "GetWeatherForecast")]
    // public IEnumerable<WeatherForecast> Get()
    // {
    //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //     {
    //         Date = DateTime.Now.AddDays(index),
    //         TemperatureC = Random.Shared.Next(-20, 55),
    //         Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //     })
    //     .ToArray();
    // }

    [HttpGet("user/{id}")]
    // [Route("getuser")]
    public string GetUserName(int id)
    {
        var u = new User { Id = 1, Name = "Nguyen Van A " + id};
        
        return _userRepo.TestUser(u);
    }

    [HttpGet("user1/{id}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(201)]
    public ActionResult<User> GetUser(int? id)
    {
        if (!id.HasValue)
        {
            return BadRequest();
        }

        // return CreatedAtAction(new User(), );
        return new User { Id = id.Value, Name = "Whatever" };

    }
}

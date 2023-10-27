using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using test_background_api.Jobs;
using test_background_api.Models;

namespace test_background_api.Controllers;

[ApiController]
[Route("[Controller]")]
public class JobController : ControllerBase
{
    private readonly JobHandler _jobHandler;

    public JobController(JobHandler jobHandler)
    {
        _jobHandler = jobHandler;
    }
    [HttpPost("message")]
    public async Task<IActionResult> Index([FromBody]MessageDto request) {
        var taskGuid = Guid.NewGuid(); //Id of this task
        //TODO : Add date on Validator 
        DateTime.TryParseExact(request.Date, "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var  date);
        //var now = DateTime.Now.AddMinutes(2).ToString("MM/dd/yyyy HH:mm");
        //var dateTimeSpan = new TimeSpan(date.Ticks);
        var jsonData = JsonSerializer.Serialize(request);
        
        if (await _jobHandler.ScheduleSaveMessageJob(date, taskGuid, jsonData)) //create job on background
        {
            return Ok( "Operation successful");
        }
        else
        {
            return BadRequest("something went wrong");
        }
    }
}
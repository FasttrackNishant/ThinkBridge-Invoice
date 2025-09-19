
using Microsoft.AspNetCore.Mvc;

namespace BuggyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetData()
        {
            string result = null;
            
            // null check addded
            if (!string.IsNullOrEmpty(result))
            {
                if (result?.Length > 0) // exception handled
                {
                    return Ok(new { message = "Data fetched" });
                }
                return BadRequest("No data");
            }
        }
    }
}

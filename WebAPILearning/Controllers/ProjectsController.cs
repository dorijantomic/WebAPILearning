using Microsoft.AspNetCore.Mvc;

namespace WebAPILearning.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Retrieved all of the projects.");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok($"Retrieved the project {id}");
        }

        [HttpGet()]
        [Route("/api/projects/{pid}/tickets")]
        public IActionResult GetProjectTicket(int pid, [FromQuery] int tid)
        {
            if (tid == 0)
            {
                return Ok($"Reading all the tickets that belong to the project ${pid}");
            }
            return Ok($"Reading project #{pid} and ticket #{tid}");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Successfully added a project.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            return Ok($"Successfully updated project #{id}.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Successfully deleted project #{id}.");
        }
    }
}
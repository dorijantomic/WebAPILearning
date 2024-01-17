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
        public IActionResult Get(int id)
        {
            return Ok($"Retrieved the project {id}");
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
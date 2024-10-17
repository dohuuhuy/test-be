using Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<ActionResult<dynamic>> GetAll()
        {
            var rs = await _blogService.All();
            return Ok(rs);
        }

        [HttpPost("CreateOrUpdate")]
        public async Task<ActionResult<BlogEntity>> CreateOrUpdate([FromBody] BlogEntity blog)
        {
            var rs = await _blogService.CreateOrUpdate(blog);
            return Ok(rs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<int>> GetByID(int id)
        {
            var rs = await _blogService.One(id);
            return Ok(rs);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAsync(int id)
        {
            var rs = await _blogService.Remove(id);
            return Ok(rs);
        }
    }
}

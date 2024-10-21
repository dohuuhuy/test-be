using Application.Service;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	[SwaggerTag("Blogs cá nhân")]
	public class BlogController : ControllerBase
	{
		private readonly BlogService _blogService;

		public BlogController(BlogService blogService)
		{
			_blogService = blogService;
		}

		[SwaggerOperation(Summary = "danh sách", Description = "Trả về chi tiết thuốc theo ID.")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var rs = await _blogService.All();
			return Ok(rs);
		}

		[HttpPost("CreateOrUpdate")]
		public async Task<IActionResult> CreateOrUpdate([FromBody] BlogEntity blog)
		{
			var rs = await _blogService.CreateOrUpdate(blog);
			return Ok(rs);
		}

		[SwaggerOperation(Summary = "Chi tiết theo ID", Description = "Trả về chi tiết theo ID.")]
		[HttpGet("{id}")]
		public async Task<ActionResult<int>> GetByID([SwaggerParameter(Description = "ID của blog")] int id)
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

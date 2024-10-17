using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Application.Service
{
    public class BlogService
    {
        private readonly AppDbContext _context;

        public BlogService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<dynamic>> CreateOrUpdate(BlogEntity blog)
        {
            try
            {
                if (blog.id == 0)
                {
                    _context.BlogEntities.Add(blog);
                    var rs = await _context.SaveChangesAsync();
                    return new Result<dynamic>(rs, "Thêm mới thành công!");
                }
                else
                {
                    var fitem = await _context.BlogEntities.FindAsync(blog.id);

                    Console.WriteLine($"{JsonConvert.SerializeObject(fitem, Formatting.Indented)}");

                    if (fitem == null)
                    {
                        return new Result<dynamic>("Không tìm thấy blog với ID đã cho.");
                    }

                    fitem.title = blog.title;
                    fitem.content = blog.content;

                    _context.BlogEntities.Update(fitem);
                    var rs = await _context.SaveChangesAsync();
                    return new Result<dynamic>(rs, "Cập nhật thành công!");
                }
            }
            catch (Exception ex)
            {
                return new Result<dynamic>(null, ex.Message);
            }
        }

        public async Task<Result<dynamic>> All()
        {
            var blogs = from blog in _context.BlogEntities select blog;
            return new Result<dynamic>(await blogs.ToListAsync(), "Thành công");
        }

        public async Task<Result<dynamic>> One(int id)
        {
            var fblog = from blog in _context.BlogEntities where blog.id == id select blog;
            return new Result<dynamic>(await fblog.FirstOrDefaultAsync());
        }

        public async Task<Result<dynamic>> Remove(int id)
        {
            var fitem = await _context.BlogEntities.FindAsync(id);

            if (fitem == null)
            {
                return new Result<dynamic>("Không tìm thấy blog với ID đã cho.");
            }
            _context.BlogEntities.Remove(fitem);
            await _context.SaveChangesAsync();
            return new Result<dynamic>("Xoá thành công!");
        }
    }

    public class FindDTO
    {
        public required string title { get; set; }
        public required string content { get; set; }
    }
}

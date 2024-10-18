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
                if (string.IsNullOrWhiteSpace(blog.title))
                {
                    throw new ArgumentException("Tiêu đề không thể trống.", nameof(blog.title));
                }

                if (blog.id == 0)
                {
                    _context.BlogEntities.Add(blog);

                    var rs = await _context.SaveChangesAsync();
                    var createdId = _context.Entry(blog).Entity.id;
                    if (rs > 0)
                        return new Result<dynamic>(createdId, "Thêm mới thành công!");
                    throw new InvalidCastException("Thêm mới không thành công!");
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
                    var createdId = _context.Entry(blog).Entity.id;
                    return new Result<dynamic>(createdId, "Cập nhật thành công!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Result<dynamic>> All()
        {
            var blogs = from blog in _context.BlogEntities select blog;
            return new Result<dynamic>(await blogs.ToListAsync(), "Thành công");
        }

        public async Task<Result<BlogEntity>> One(int id)
        {
            var query =
                from blog in _context.BlogEntities
                where blog.id == id
                select EntityHelper.Omit(blog, "content");

            var act = await query.FirstOrDefaultAsync();
            return new Result<BlogEntity>(act);
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
}

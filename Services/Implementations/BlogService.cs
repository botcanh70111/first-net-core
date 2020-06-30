using Infrastructure.Data;
using Services.Interfaces;
using Services.Mappers;
using Services.Models;

namespace Services.Implementations
{
    public class BlogService : BaseService<Blogs, Blog>, IBlogService
    {
        public BlogService(ApplicationDbContext context, IModelMapper mapper) : base(context, mapper)
        {
        }
    }
}

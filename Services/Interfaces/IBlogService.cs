using Infrastructure.Data;
using Services.Models;

namespace Services.Interfaces
{
    public interface IBlogService : IAppService<Blogs, Blog>
    {
    }
}

using Infrastructure.Data;
using Services.Models;
using System;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IBlogService : IAppService<Blogs, Blog>
    {
        IEnumerable<Blog> GetBlogsByBloggerId(string bloggerId);
        bool IsUrlExisted(string blogUrl, Guid id, string bloggerId);
    }
}

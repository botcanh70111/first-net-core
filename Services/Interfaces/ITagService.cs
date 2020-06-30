using Infrastructure.Data;
using Services.Models;

namespace Services.Interfaces
{
    public interface ITagService : IAppService<Tags, Tag>
    {
        bool IsExisted(string tag);
        bool IsUrlExisted(string url);
    }
}

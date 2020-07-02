using Infrastructure.Data;
using Services.Models;
using System;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface ITagService : IAppService<Tags, Tag>
    {
        bool IsExisted(string tag, Guid tagId, string ownerId);
        bool IsUrlExisted(string url, Guid tagId, string ownerId);
        IEnumerable<Tag> GetTagsByOwnerId(string ownerId);
    }
}

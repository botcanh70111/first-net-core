using Infrastructure.Data;
using Services.Extensions;
using Services.Interfaces;
using Services.Mappers;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Implementations
{
    public class TagService : BaseService<Tags, Tag>, ITagService
    {
        public TagService(ApplicationDbContext context, IModelMapper mapper) : base(context, mapper)
        {
        }

        public override Tag Create(Tag model, bool forceSave = true)
        {
            MakeUrl(model);
            return base.Create(model, forceSave);
        }

        public override Tag Update(Tag model, bool forceSave = true)
        {
            MakeUrl(model);
            return base.Update(model, forceSave);
        }

        public bool IsExisted(string tag, Guid tagId, string ownerId)
        {
            return _context.Tags.FirstOrDefault(x => x.Name == tag && x.OwnerId == ownerId && (x.Id != tagId || tagId == Guid.Empty)) != null;
        }

        public bool IsUrlExisted(string url, Guid tagId, string ownerId)
        {
            return _context.Tags.FirstOrDefault(x => x.TagUrl == url && x.OwnerId == ownerId && (x.Id != tagId || tagId == Guid.Empty)) != null;
        }

        private void MakeUrl(Tag model)
        {
            if (string.IsNullOrEmpty(model.TagUrl))
                model.TagUrl = model.Name.ToAliasUrl();
            else
                model.TagUrl = model.TagUrl.ToAliasUrl();

            if (IsUrlExisted(model.TagUrl, model.Id, model.OwnerId))
                model.TagUrl = model.TagUrl + "-" + DateTime.Now.Ticks;
        }

        public IEnumerable<Tag> GetTagsByOwnerId(string ownerId)
        {
            var tags = _context.Tags.Where(x => x.OwnerId == ownerId);
            return _mapper.Map<IEnumerable<Tag>>(tags);
        }
    }
}

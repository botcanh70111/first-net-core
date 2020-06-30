using Infrastructure.Data;
using Services.Extensions;
using Services.Interfaces;
using Services.Mappers;
using Services.Models;
using System;
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
            if (string.IsNullOrEmpty(model.TagUrl))
                model.TagUrl = model.Name.ToAliasUrl();

            if (IsUrlExisted(model.TagUrl)) 
                model.TagUrl = model.TagUrl + "-" + DateTime.Now.ToOADate();

            return base.Create(model, forceSave);
        }

        public bool IsExisted(string tag)
        {
            return _context.Tags.FirstOrDefault(x => x.Name == tag) != null;
        }

        public bool IsUrlExisted(string url)
        {
            return _context.Tags.FirstOrDefault(x => x.TagUrl == url) != null;
        }
    }
}

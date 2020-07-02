﻿using Infrastructure.Data;
using Services.Extensions;
using Services.Interfaces;
using Services.Mappers;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Implementations
{
    public class BlogService : BaseService<Blogs, Blog>, IBlogService
    {
        public BlogService(ApplicationDbContext context, IModelMapper mapper) : base(context, mapper)
        {
        }

        public IEnumerable<Blog> GetBlogsByBloggerId(string bloggerId)
        {
            var blogs = _context.Blogs.Where(x => x.BloggerId == bloggerId);
            return _mapper.Map<IEnumerable<Blog>>(blogs);
        }

        public override Blog GetById(object id)
        {
            var model = new Blog();
            var entityBlog = (from blog in _context.Blogs
                             join blogtag in _context.BlogsTags on blog.Id equals blogtag.BlogId into BlogTags
                             from b in BlogTags.DefaultIfEmpty()
                             where blog.Id == (Guid)id
                             select new { Blog = blog, Tags = b })
                             .ToLookup(x => x.Blog)
                             .Select(x => new { Blog = x.Key, Tags = x.Select(t => t.Tags).Where(t => t != null).ToList() })
                             .FirstOrDefault();

            model = _mapper.Map<Blog>(entityBlog.Blog);
            model.BlogTagIds = entityBlog.Tags != null ? entityBlog.Tags.Select(x => x.TagId).ToList() : new List<Guid>();
            return model;
        }

        public override Blog Create(Blog model, bool forceSave = true)
        {
            MakeUrl(model);
            var blog = _mapper.Map<Blogs>(model);
            _context.Blogs.Add(blog);
            var blogTags = new List<BlogsTags>();
            foreach (var t in model.BlogTagIds) blogTags.Add(new BlogsTags { BlogId = blog.Id, TagId = t });
            _context.BlogsTags.AddRange(blogTags);
            if (forceSave) _context.SaveChanges();
            
            return GetById(blog.Id);
        }

        public override Blog Update(Blog blog, bool forceSave = true)
        {
            MakeUrl(blog);
            var entityBlog = _context.Blogs.Find(blog.Id);
            var blogTags = new List<BlogsTags>();
            foreach (var t in blog.BlogTagIds) blogTags.Add(new BlogsTags { BlogId = blog.Id, TagId = t });
            _mapper.Map<Blog, Blogs>(blog, entityBlog);
            _context.Blogs.Update(entityBlog);
            _context.BlogsTags.RemoveRange(_context.BlogsTags.Where(x => x.BlogId == blog.Id));
            _context.BlogsTags.AddRange(blogTags);
            if (forceSave) _context.SaveChanges();

            return GetById(blog.Id);
        }

        private void MakeUrl(Blog model)
        {
            if (string.IsNullOrEmpty(model.BlogUrl))
                model.BlogUrl = model.Name.ToAliasUrl();
            else
                model.BlogUrl = model.BlogUrl.ToAliasUrl();

            if (IsUrlExisted(model.BlogUrl, model.Id, model.BloggerId))
                model.BlogUrl = model.BlogUrl + "-" + DateTime.Now.Ticks;
        }

        public bool IsUrlExisted(string blogUrl, Guid id, string bloggerId)
        {
            return _context.Blogs.FirstOrDefault(x => x.BlogUrl == blogUrl && x.BloggerId == bloggerId && (id == Guid.Empty || x.Id != id)) != null;
        }

        public override bool Delete(object id, bool forceSave = true)
        {
            _context.BlogsTags.RemoveRange(_context.BlogsTags.Where(x => x.BlogId == (Guid)id));
            return base.Delete(id, forceSave);
        }
    }
}

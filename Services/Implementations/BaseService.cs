using Infrastructure.Data;
using Services.Mappers;
using System;
using System.Collections.Generic;

namespace Services.Implementations
{
    public class BaseService<T, TModel> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IModelMapper _mapper;
        public BaseService(ApplicationDbContext context,
            IModelMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual TModel Create(TModel model, bool forceSave = true)
        {
            var entity = _mapper.Map<T>(model);
            var addedEntity = _context.Set<T>().Add(entity).Entity;
            if (forceSave) {
                _context.SaveChanges();
            }
            return _mapper.Map<TModel>(addedEntity);
        }

        public TModel Update(TModel config, bool forceSave = true)
        {
            var entity = _mapper.Map<T>(config);
            var update = _context.Set<T>().Update(entity);
            var updatedEntity = update.Entity;
            if (forceSave)
            {
                _context.SaveChanges();
            }
            return _mapper.Map<TModel>(updatedEntity);
        }

        public virtual bool Delete(object id, bool forceSave = true)
        {
            try
            {
                var entity = _context.Set<T>().Find(id);
                _context.Set<T>().Remove(entity);
                if (forceSave)
                {
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public virtual IEnumerable<TModel> GetAll()
        {
            var query = _context.Set<T>();
            return _mapper.Map<IEnumerable<TModel>>(query);
        }

        public virtual TModel GetById(object id)
        {
            var entity = _context.Set<T>().Find(id);
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            return _mapper.Map<TModel>(entity);
        }
    }
}

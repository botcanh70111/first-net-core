using System;
using System.Collections.Generic;

namespace Services.Interfaces
{
    /// <summary>
    /// Service interface
    /// </summary>
    /// <typeparam name="T">Entity model</typeparam>
    /// <typeparam name="TModel">Model</typeparam>
    public interface IAppService<T, TModel>
    {
        IEnumerable<TModel> GetAll();
        TModel GetById(object id);
        TModel Create(TModel model, bool forceSave = true);
        TModel Update(TModel model, bool forceSave = true);
        bool Delete(Guid id, bool forceSave = true);
    }
}

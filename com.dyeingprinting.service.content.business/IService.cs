using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.dyeingprinting.service.content.data.Model;
using Com.Moonlay.Models;

namespace com.dyeingprinting.service.content.business
{
    public interface IService<TModel>
    {
        List<TModel> Find();
        Task<List<TModel>> FindAsync();
        Task<int> Create(TModel model);
        Task<int> Delete(int id);
        Task<TModel> GetSingleById(int id);
        Task<int> Update(TModel dbmodel, TModel model);
    }
}

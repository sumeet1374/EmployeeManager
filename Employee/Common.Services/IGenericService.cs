using Common.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Services
{
    public interface IGenericService<T>
    {
        void Create(T obj);
        void Update(T obj);
        void Deactivate(int id);
        T FindById(int id);
        PagedResult<T> FindAll(int pageNumber,int pageSize);
    }
}

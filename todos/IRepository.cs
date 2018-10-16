using System;
using System.Collections.Generic;

namespace Todos
{
    public interface IRepository<T>
    {
        void Create(T taskList);
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        void Update(T item);
        void Delete(Guid id);
    }
}
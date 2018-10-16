using System;
using System.Collections.Generic;
using Humanizer;
using LiteDB;
using Todos.Models;

namespace Todos
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private string _collectionName;

        public Repository()
        {
            _collectionName = typeof(T).Name.Pluralize().Underscore();
        }
        
        public void Create(T item) => Execute(col => col.Insert(item));
        public T GetById(Guid id) => Execute(col => col.FindOne(item => item.Id == id));
        public IEnumerable<T> GetAll() => Execute(col => col.FindAll());
        public void Update(T item) => Execute(col => col.Update(item));
        public void Delete(Guid id) => Execute(col => col.Delete(item => item.Id == id));

        private TRet Execute<TRet>(Func<LiteCollection<T>, TRet> func)
        {
            using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
            {
                var col = db.GetCollection<T>(_collectionName);
                return func.Invoke(col);
            }
        }
    }
}
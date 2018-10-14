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
        
        public void Create(T item)
        {
            using (var db = Db())
            {
                var col = db.GetCollection<T>(_collectionName);
                col.Insert(item);
            }
        }

        public T GetById(Guid id)
        {
            using (var db = Db())
            {
                var col = db.GetCollection<T>(_collectionName);
                return col.FindOne(item => item.Id == id);
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (var db = Db())
            {
                var col = db.GetCollection<T>(_collectionName);
                return col.FindAll();
            }
        }

        public void Update(T item)
        {
            using (var db = Db())
            {
                var col = db.GetCollection<T>(_collectionName);
                col.Update(item);
            }
        }
        
        
        private static LiteDatabase Db()
        {
            return new LiteDatabase(@"C:\Temp\MyData.db");
        }
    }
    
}
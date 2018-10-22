using System;
using System.Reflection;

namespace Todos.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        
    }
}
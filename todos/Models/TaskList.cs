using System;

namespace Todos.Models
{
    public class TaskList : Entity
    {
        public TaskList()
        {
            Id = Guid.NewGuid();
        }
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
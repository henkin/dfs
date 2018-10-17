using System;
using Todos.Models;

namespace todos.Models
{
    public class TodoTask : Entity
    {
        public Guid TaskListId { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
    }
}
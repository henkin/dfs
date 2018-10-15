using System;

namespace Todos.Models
{
    public class TaskList : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
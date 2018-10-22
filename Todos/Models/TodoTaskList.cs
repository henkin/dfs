using System;

namespace Todos.Models
{
    public class TodoTaskList : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
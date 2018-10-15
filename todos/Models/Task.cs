using System;
using Todos.Models;

namespace todos.Models
{
    public class Task : Entity
    {
        public string Name { get; set; }
        public bool Completed { get; set; }
    }
}
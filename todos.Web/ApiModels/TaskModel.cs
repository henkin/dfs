using System;
using System.Collections.Generic;
using System.Linq;
using todos.Models;

namespace todos.Web.ApiModels
{
    public class TaskModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }

        public TaskModel()
        {
            Id = Guid.NewGuid();
        }

        

        public override string ToString()
        {
            return $"{Id}: {Name}, completed: {Completed}";
        }
    }
}
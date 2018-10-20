using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using todos.Models;
using Todos.Models;

namespace todos.Web.ApiModels
{
    public class TaskListModel 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TaskModel> Tasks { get; set; }

        public TaskListModel()
        {
            Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return $"{Id}: {Name}, {Description}, \n {string.Join("\n ", Tasks)}";
        }
    }
}
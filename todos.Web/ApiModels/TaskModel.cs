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

        public static List<TaskModel> FromTodoTasks(IEnumerable<TodoTask> todoTasks)
        {
            return todoTasks.Select(task => new TaskModel
            {
                Id = task.Id,
                Name = task.Name,
                Completed = task.Completed
            }).ToList();
        }
    }
}
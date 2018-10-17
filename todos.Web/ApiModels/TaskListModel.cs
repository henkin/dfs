using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public static TaskListModel FromTodoTaskList(TodoTaskList list, IEnumerable<TodoTask> todoTasks)
        {
            return new TaskListModel()
            {
                Id = list.Id,
                Description = list.Description,
                Tasks = TaskModel.FromTodoTasks(todoTasks)
            };
        }
    }
}
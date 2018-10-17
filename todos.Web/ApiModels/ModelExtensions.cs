using System;
using todos.Models;
using Todos.Models;

namespace todos.Web.ApiModels
{
    public static class ModelExtensions
    {
        public static TodoTaskList ToTodoTaskList(this TaskListModel model)
        {
            return AutoMapper.Mapper.Map<TodoTaskList>(model);
        }

        public static TodoTask ToTodoTask(this TaskModel model, Guid listId)
        {
            var task = AutoMapper.Mapper.Map<TodoTask>(model);
            task.TaskListId = listId;
            return task;
        }
    }
}
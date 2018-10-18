using System;
using System.Collections.Generic;
using AutoMapper;
using todos.Models;
using Todos.Models;

namespace todos.Web.ApiModels
{
    public static class ModelExtensions
    {
        static ModelExtensions()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<TaskListModel, TodoTaskList>();
                cfg.CreateMap<TodoTaskList, TaskListModel>();
                cfg.CreateMap<TaskModel, TodoTask>();
            });
        }

        public static TodoTaskList ToTodoTaskList(this TaskListModel model)
        {
            return Mapper.Map<TodoTaskList>(model);
        }

        public static TodoTask ToTodoTask(this TaskModel model, Guid listId)
        {
            var task = Mapper.Map<TodoTask>(model);
            task.TaskListId = listId;
            return task;
        }

        public static TaskListModel FromTodoTaskList(this TodoTaskList list, IEnumerable<TodoTask> todoTasks)
        {
            var model = Mapper.Map<TaskListModel>(list);
            model.Tasks = TaskModel.FromTodoTasks(todoTasks);
            return model;
        }
    }
}
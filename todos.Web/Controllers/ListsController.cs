using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.Annotations;
using todos.Models;
using todos.Web.ApiModels;
using Todos.Models;

namespace Todos.Web.Controllers
{
    // https://app.swaggerhub.com/apis/aweiker/ToDo/1.0.0
    [Route("lists")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        private readonly IRepository<TodoTaskList> _listRepository;
        private readonly IRepository<TodoTask> _taskRepository;

        public ListsController(IRepository<TodoTaskList> listRepository, IRepository<TodoTask> taskRepository)
        {
            _listRepository = listRepository;
            _taskRepository = taskRepository;
        }
        
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<TaskListModel>> Get(string search, int? skip, int? limit)
        {
            try
            {
                var taskLists = _listRepository.GetAll().ToList();
                
                // in real, production code, this filtering would be pushed down the the persistence layer queries.
                if (!string.IsNullOrEmpty(search))
                {
                    taskLists = taskLists.Where(l => l.Name == search).ToList();
                }

                if (skip.HasValue) taskLists = taskLists.Skip(skip.Value).ToList();
                if (limit.HasValue) taskLists = taskLists.Take(limit.Value).ToList();
                
                var tasks = _taskRepository.GetAll().ToList();
                
                var listsWithTasks = taskLists.Select(list => 
                    list.FromTodoTaskList(tasks.Where(t => t.TaskListId == list.Id))
                    ).ToList();
                return new JsonResult(listsWithTasks);
            }
            catch (Exception ex)
            {
                throw;
            }               
        }

        // GET /lists/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(Guid id)
        {
            try
            {
                var taskList = _listRepository.GetById(id);
                var tasks = _taskRepository.Find(t => t.TaskListId == id).ToList();
                return new JsonResult(taskList.FromTodoTaskList(tasks));
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.NotFound);
            } 
        }

        // POST /lists
        [HttpPost]
        public ActionResult Post([FromBody] TaskListModel taskList)
        {
            try
            {
                var list = taskList.ToTodoTaskList();
                _listRepository.Create(list);
                var tasks = taskList.Tasks.Select(t => t.ToTodoTask(list.Id)).ToList();
                tasks.ForEach(t => _taskRepository.Create(t));
                taskList.Tasks = tasks.FromTodoTasks();
                return CreatedAtAction("Get", new {id = taskList.Id}, taskList);
            }
            catch (Exception ex)
            {
                throw;
            } 
        }

//        // PUT api/values/5
//        [HttpPut("{id}")]
//        public void Put(Guid id, [FromBody] TodoTaskList todoTaskList)
//        {
//            try
//            {
//                _repository.Update(todoTaskList);
//                Ok();
//            }
//            catch (Exception ex)
//            {
//                throw;
//            } 
//        }

//        // DELETE api/values/5
//        [HttpDelete("{id}")]
//        public void Delete(Guid id)
//        {
//            try
//            {
//                _repository.Delete(id);
//                Ok();
//            }
//            catch (Exception ex)
//            {
//                throw;
//            } 
//        }
        
//        [HttpGet("/")]
//        [Produces("text/html")]
//        public ContentResult About()
//        {
//            return Content(@"<html><body>
//                <ul>
//                    <li><a href='swagger'>swagger</a>
//                    <li><a href='lists'>lists</a>
//                </ul>
//            </body></html>", "text/html");
//        }
    }
}

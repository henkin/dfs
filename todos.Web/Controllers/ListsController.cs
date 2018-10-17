using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using todos.Models;
using todos.Web.ApiModels;
using Todos.Models;

namespace Todos.Web.Controllers
{
    // https://app.swaggerhub.com/apis/aweiker/ToDo/1.0.0
    [Route("[controller]")]
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
        
        // GET /Lists
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<TaskListModel>> Get()
        {
            try
            {
                var taskLists = _listRepository.GetAll();
                var tasks = _taskRepository.GetAll();
                var listsWithTasks = taskLists.Select(list => 
                    TaskListModel.FromTodoTaskList(list, tasks.Where(t => t.TaskListId == list.Id))
                    ).ToList();
                return new JsonResult(listsWithTasks);
            }
            catch (Exception ex)
            {
                throw;
                //return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }               
        }

        // GET /Lists/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(Guid id)
        {
            try
            {
                var taskList = _listRepository.GetById(id);
                var tasks = _taskRepository.Find(t => t.TaskListId == id);
                return new JsonResult(TaskListModel.FromTodoTaskList(taskList, tasks));
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.NotFound);
            } 
        }

        // POST /Lists
        [HttpPost]
        public ActionResult Post([FromBody] TaskListModel taskList)
        {
            try
            {
                var list = taskList.ToTodoTaskList();
                _listRepository.Create(list);
                var tasks = taskList.Tasks.Select(t => t.ToTodoTask(list.Id)).ToList();
                tasks.ForEach(t => _taskRepository.Create(t));
                
                return CreatedAtAction("Get", new {id = taskList.Id}, taskList);
                //Ok();
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
        
        [HttpGet("/")]
        [Produces("text/html")]
        public ContentResult About()
        {
            return Content("<html><body><a href='Lists'>Lists</a></body></html>", "text/html");
        }
    }
}

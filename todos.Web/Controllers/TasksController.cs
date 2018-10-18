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
    public class TasksController : ControllerBase
    {
//        private readonly IRepository<TodoTaskList> _listRepository;
        private readonly IRepository<TodoTask> _taskRepository;

        public TasksController(IRepository<TodoTask> taskRepository)
        {
            //_listRepository = listRepository;
            _taskRepository = taskRepository;
        }
        
        // GET /Lists
        [HttpGet]
        public ActionResult<IEnumerable<TaskListModel>> Get()
        {
            try
            {
                var tasks = _taskRepository.GetAll();
                return new JsonResult(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.NotFound);
                //return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }               
        }

        // GET /Lists/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(Guid id)
        {
            try
            {
                var task = _taskRepository.GetById(id);
                return new JsonResult(task);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.NotFound);
            } 
        }

        // POST /Lists
        [HttpPost]
        public ActionResult Post([FromBody] TaskModel taskList)
        {
            try
            {
                var task = taskList.ToTodoTask();
                _taskRepository.Create(task);
                
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using todos.Models;
using todos.Web.ApiModels;

namespace Todos.Web.Controllers
{
    // https://app.swaggerhub.com/apis/aweiker/ToDo/1.0.0
    [Route("tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IRepository<TodoTask> _taskRepository;

        public TasksController(IRepository<TodoTask> taskRepository)
        {
            _taskRepository = taskRepository;
        }
        
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
            }               
        }

        // GET /tasks
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

        // POST /tasks
        [HttpPost]
        public ActionResult Post([FromBody] TaskModel todoTask)
        {
            try
            {
                var task = todoTask.ToTodoTask();
                _taskRepository.Create(task);
                
                return CreatedAtAction("Get", new {id = todoTask.Id}, todoTask);
                //Ok();
            }
            catch (Exception ex)
            {
                throw;
            } 
        }

        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] TaskModel taskModel)
        {
            try
            {
                // this 
                var todoTask = taskModel.ToTodoTask();
                _taskRepository.Update(todoTask);
                
                return CreatedAtAction("Get", new {id = taskModel.Id}, todoTask);
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
    }
}

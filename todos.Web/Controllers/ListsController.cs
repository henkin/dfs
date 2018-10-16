using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Todos.Models;

namespace Todos.Web.Controllers
{
    // https://app.swaggerhub.com/apis/aweiker/ToDo/1.0.0
    [Route("[controller]")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        private readonly IRepository<TaskList> _repository;

        public ListsController(IRepository<TaskList> repository)
        {
            _repository = repository;
        }
        
        // GET /Lists
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<TaskList>> Get()
        {
            try
            {
                var taskLists = _repository.GetAll();
                return new JsonResult(taskLists);
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
                var taskLists = _repository.GetById(id);
                return new JsonResult(taskLists);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.NotFound);
            } 
        }

        // POST /Lists
        [HttpPost]
        public ActionResult Post([FromBody] TaskList taskList)
        {
            try
            {
                _repository.Create(taskList);
                return CreatedAtAction("Get", new {id = taskList.Id}, taskList);
                //Ok();
            }
            catch (Exception ex)
            {
                throw;
            } 
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] TaskList taskList)
        {
            try
            {
                _repository.Update(taskList);
                Ok();
            }
            catch (Exception ex)
            {
                throw;
            } 
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            try
            {
                _repository.Delete(id);
                Ok();
            }
            catch (Exception ex)
            {
                throw;
            } 
        }
        
        [HttpGet("/")]
        [Produces("text/html")]
        public ContentResult About()
        {
            return Content("<html><body><a href='Lists'>Lists</a></body></html>", "text/html");
        }
    }
}

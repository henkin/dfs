using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
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
        // GET api/values
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
                return NotFound();
//                var responseMessage = new ResponseMessage<List<string>>(errors, HttpStatusCode.BadRequest);
//                throw new HttpResponseException(responseMessage);                
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        
        [HttpGet("/")]
        [Produces("text/html")]
        public ContentResult About()
        {
            return Content("<html><body><a href='Lists'>Lists</a></body></html>", "text/html");
        }
    }
}

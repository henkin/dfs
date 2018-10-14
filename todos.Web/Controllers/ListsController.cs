using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Todos.Models;

namespace todos.Web.Controllers
{
    // https://app.swaggerhub.com/apis/aweiker/ToDo/1.0.0
    [Route("[controller]")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        public ListsController()
        {
            
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<TaskList>> Get()
        {
            return new[] { new TaskList { Name = "foo", Description = "boo"} };
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
    }
}

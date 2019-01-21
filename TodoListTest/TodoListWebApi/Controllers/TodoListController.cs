using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList;

namespace TodoListWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private TaskManager _taskManager;

        public TodoListController(TaskManager taskManager)
        {
            _taskManager = taskManager;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ITodoTask>> Get()
        {
            return Ok(_taskManager.GetAll());
        }

        // POST api/values
        [HttpPost("{email}")]
        public ActionResult<IEnumerable<ITodoTask>> Post(string email, [FromBody]TaskContentModel model)
        {
            _taskManager.Add(new TodoTask(email) { Content = model.Content });
            return Ok(_taskManager.GetAll());
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<IEnumerable<ITodoTask>> Put(Guid id, [FromBody] TaskContentModel model)
        {
            _taskManager.Update(id, model.Content);
            return Ok(_taskManager.GetAll());
        }

        public class TaskContentModel
        {
            public string Content { get; set; }
        }

        [HttpPut("SetComplete/{id}")]
        public ActionResult<IEnumerable<ITodoTask>> SetComplete(Guid id)
        {
            _taskManager.MarkComplete(id);
            return Ok(_taskManager.GetAll());
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<ITodoTask>> Delete(Guid id)
        {
            _taskManager.Remove(id);
            return Ok(_taskManager.GetAll());
        }
    }
}

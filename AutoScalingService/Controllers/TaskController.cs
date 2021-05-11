using AutoScalingService.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;

namespace AutoScalingService.Controllers
{
    public class TaskController : ApiController
    {
        
        private readonly ITaskManager taskManager;
        public TaskController() : this(WebApiApplication.TaskManager)
        {

        }

        public TaskController(ITaskManager taskManager)
        {
            this.taskManager = taskManager;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        public string Perform(int id)
        {
            if(id < 0)
                throw new Exception("invalid params");
            return this.taskManager.Perform(id);
        }

        [HttpGet]
        // GET api/values/5
        public string Notify(string id)
        {
            return this.taskManager.Notify(id);
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

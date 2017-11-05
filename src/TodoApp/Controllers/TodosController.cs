using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Persistence;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    public class TodosController : Controller
    {
        private readonly ITodoStore _store;

        public TodosController(ITodoStore store) {
          _store = store;
        }

        // GET: api/todos
        [HttpGet]
        public IEnumerable<Todo> GetAll()
        {
            return _store.GetAll();
        }

        // POST: api/todos
        [HttpPost]
        public void AddTodo([FromBody] string description)
        {
            _store.Add(description);
        }

        // PUT: api/todos/toggle/{id}
        [HttpPut("toggle/{id}")]
        public void ToggleTodo(string id)
        {
            var parsedId = Guid.Parse(id);

            _store.Toggle(parsedId);
        }

        // DELETE: api/todos/clear
        [HttpDelete("clear")]
        public void Clear()
        {
            _store.Clear();
        }
    }
}

using System;
using System.Collections.Generic;
using TodoApp.Models;

namespace TodoApp.Persistence
{
  public class TodoStore : ITodoStore
  {
    private List<Todo> Todos = new List<Todo>();

    public List<Todo> GetAll()
    {
      return Todos;
    }

    public void Add(string description)
    {
      var todo = new Todo
      {
        Id = Guid.NewGuid(),
        Description = description
      };

      Todos.Add(todo);
    }

    public void Toggle(Guid id)
    {
      var todo = Get(id);

      todo.IsCompleted = !todo.IsCompleted;
    }

    public void Clear()
    {
      Todos.RemoveAll(todo => todo.IsCompleted);
    }

    private Todo Get(Guid id)
    {
      var todo = Todos.Find(t => t.Id == id);

      if (todo == null) {
        throw new Exception($"Todo with id: '{id.ToString()}' not found!");
      }

      return todo;
    }
  }
}

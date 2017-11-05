using System;
using System.Collections.Generic;
using TodoApp.Models;

namespace TodoApp.Persistence
{
  public interface ITodoStore 
  {
    List<Todo> GetAll();

    void Add(string description);

    void Toggle(Guid id);

    void Clear();
  }
}

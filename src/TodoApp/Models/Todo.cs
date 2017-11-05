using System;

namespace TodoApp.Models {
  public class Todo {
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public Guid Id { get; set; }
  }
}

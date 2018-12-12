using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList
{
    public class TodoTask : ITodoTask
    {
        public TodoTask(string email)
        {
            Email = email;
        }
        public Guid Id { get; } = Guid.NewGuid();
        public string Email { get; }
        public string Content { get; set; }
        public bool IsComplete { get; set; }
    }
}

using System;

namespace TodoList
{
    public interface ITodoTask
    {
        Guid Id { get; }
        string Content { get; set; }
    }
}
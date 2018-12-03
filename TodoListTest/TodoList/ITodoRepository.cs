using System;

namespace TodoList
{
    public interface ITodoRepository
    {
        void Add(ITodoTask task);
        ITodoTask Get(Guid id);
    }
}
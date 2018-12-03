using System;
using System.Collections.Generic;

namespace TodoList
{
    public interface ITodoRepository
    {
        void Add(ITodoTask task);
        ITodoTask Get(Guid id);
        void Remove(Guid id);
        IEnumerable<ITodoTask> GetAll();
    }
}
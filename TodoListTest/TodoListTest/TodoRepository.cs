using System;
using System.Collections.Generic;
using System.Linq;
using TodoList;

namespace TodoListTest
{
    public class TodoRepository : ITodoRepository
    {
        List<ITodoTask> _tasks = new List<ITodoTask>();
        public void Add(ITodoTask task)
        {
            _tasks.Add(task);
        }

        public ITodoTask Get(Guid id)
        {
            return _tasks.FirstOrDefault(x=>x.Id == id);
        }

        public IEnumerable<ITodoTask> GetAll()
        {
            return _tasks;
        }

        public void Remove(Guid id)
        {
            var task = Get(id);
            _tasks.Remove(task);
        }
    }
}
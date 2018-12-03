using System;

namespace TodoList
{
    public class TaskManager
    {
        private readonly ITodoRepository _repository;

        public TaskManager(ITodoRepository repository)
        {
            _repository = repository;
        }

        public void Add(ITodoTask task)
        {
            _repository.Add(task);
        }
    }
}
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
        public void Update(Guid id, string content)
        {
            var task = _repository.Get(id);
            task.Content = content;
        }

        public void Remove(Guid id)
        {
            _repository.Remove(id);
        }
    }
}
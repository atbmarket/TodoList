using System;
using System.Collections;
using System.Collections.Generic;

namespace TodoList
{
    public class TaskManager
    {
        private readonly ITodoRepository _repository;
        private readonly ITaskNotifier _notifier;

        public TaskManager(ITodoRepository repository, ITaskNotifier notifier)
        {
            _repository = repository;
            _notifier = notifier;
        }
        public void Add(ITodoTask task)
        {
            _repository.Add(task);
        }
        public void Update(Guid id, string content)
        {
            var task = _repository.Get(id);
            task.Content = content;
            _notifier.Notify(task);
        }

        public void Remove(Guid id)
        {
            _repository.Remove(id);
        }

        public void MarkComplete(Guid id)
        {
            var task = _repository.Get(id);
            task.IsComplete = true;
            _notifier.Notify(task);
        }

        public IEnumerable<ITodoTask> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
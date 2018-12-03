using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using NSubstitute;
using System;
using TodoList;
using Xunit;

namespace TodoListTest
{
    public class TodoListTest
    {
        [Theory]
        [NSubData]
        public void should_create_task(ITodoTask task, [Frozen]ITodoRepository repository, TaskManager manager)
        {
            // act
            manager.Add(task);
            //assert
            repository.Received().Add(task);
        }
        [Theory]
        [NSubData]
        public void should_change_task(ITodoTask task, [Frozen]ITodoRepository repository, TaskManager manager, string newContent)
        {
            // arrange            
            repository.Get(task.Id).Returns(task);
            
            // act
            manager.Update(task.Id, newContent);
            
            //assert
            repository.Received().Get(task.Id);
            task.Content.Should().Be(newContent);
        }

        [Theory]
        [NSubData]
        public void should_delete_task(ITodoTask task, [Frozen]ITodoRepository repository, TaskManager manager)
        {
            // act
            manager.Remove(task.Id);

            //assert
            repository.Received().Remove(task.Id);
        }
        [Theory]
        [NSubData]
        public void should_mark_complete_task(ITodoTask task, [Frozen]ITodoRepository repository, TaskManager manager)
        {
            // arrange            
            repository.Get(task.Id).Returns(task);

            // act
            manager.MarkComplete(task.Id);

            //assert
            repository.Received().Get(task.Id);
            task.IsComplete.Should().BeTrue();
        }
        [Theory]
        [NSubData]
        public void should_return_all_tasks([Frozen]ITodoRepository repository, TaskManager manager)
        {
            // action
            manager.GetAll();
            //assert
            repository.Received().GetAll();
        }
    }
}

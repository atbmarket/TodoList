using AutoFixture;
using FluentAssertions;
using NSubstitute;
using System;
using TodoList;
using Xunit;

namespace TodoListTest
{
    public class TodoListTest
    {
        Fixture fixture = new Fixture();
        [Fact]
        public void should_create_task()
        {
            // arrange            
            var repository = Substitute.For<ITodoRepository>();
            var manager = new TaskManager(repository);
            var task = Substitute.For<ITodoTask>();
            // act
            manager.Add(task);
            //assert
            repository.Received().Add(task);
        }
        [Fact]
        public void should_change_task()
        {
            // arrange            
            var repository = Substitute.For<ITodoRepository>();
            var manager = new TaskManager(repository);
            var task = Substitute.For<ITodoTask>();
            var newContent = "new content";
            repository.Get(task.Id).Returns(task);
            
            // act
            manager.Update(task.Id, newContent);
            
            //assert
            repository.Received().Get(task.Id);
            task.Content.Should().Be(newContent);
        }

        [Fact]
        public void should_delete_task()
        {
            // arrange            
            var repository = Substitute.For<ITodoRepository>();
            var manager = new TaskManager(repository);
            var task = Substitute.For<ITodoTask>();

            // act
            manager.Remove(task.Id);

            //assert
            repository.Received().Remove(task.Id);
        }
    }
}

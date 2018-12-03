using AutoFixture;
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


    }
}

using Autofac;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TodoListWebApi;
using TodoListWebApi.Controllers;
using Xunit;

namespace TodoListTest
{
    public class DiContainerTests
    {
        public static IEnumerable<object[]> ControllerTypes
        {
            get
            {
                return typeof(Startup).Assembly.GetTypes()
                    .Where(_ => typeof(ControllerBase).IsAssignableFrom(_))
                    .Select(_ => new object[] {_});
            }
        }

        [Theory]
        [MemberData(nameof(ControllerTypes))]
        public void controller_should_be_creatable(Type contollerType)
        {    
            Startup.CreateContainerBuilder().Build()
                .Invoking(_ => _.Resolve(contollerType)).Should().NotThrow(); 
        }
    }
}


using ApprovalTests.Email;
using ApprovalTests.Reporters;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using TodoList;
using Xunit;

[assembly: FrontLoadedReporter(typeof(BeyondCompare4Reporter))]
namespace TodoListTest
{
    [UseReporter(typeof(BeyondCompare4Reporter))]
    public class EmailMessageMapperTest
    {
        [Fact]
        public void should_approve_email_message()
        {
            // arrange
            var mapper = Substitute.For<IMapper<MailMessage, ITodoTask>>();            
            var task = Substitute.For<ITodoTask>();
            mapper.Map(task).Returns(new MailMessage("src@aa.ss", "to@aa.ss", "subject", "body"));

            // act
            var msg = mapper.Map(task);
            // assert
            EmailApprovals.Verify(msg);
        }
    }
}

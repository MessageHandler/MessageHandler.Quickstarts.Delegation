using API.Controllers;
using MessageHandler.Quickstart.Contract;
using MessageHandler.Runtime.AtomicProcessing;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace ComponentTests
{
    public class WhileDelegatingNotifyBuyerThroughAPI
    {
        [Fact]
        public async Task GivenWellknownCommand_WhenNotifyBuyer_ThenNotifyBuyerIsDelegated()
        {
            // given
            var command = new NotifyBuyerCommandBuilder()
                            .WellknownCommand("19c05669-8ee3-4d06-b2ad-94d301d7b1c4")
                            .Build();

            var mock = new Mock<IDispatchMessages>();
            mock.Setup(dispatcher => dispatcher.Dispatch(It.IsAny<NotifyBuyer>()));

            // when
            var controller = new CommandController(mock.Object);

            var actionResult = await controller.Notify(command);

            // then
            mock.Verify(dispatcher => dispatcher.Dispatch(It.Is<NotifyBuyer>(cmd => cmd.To == command.To)), Times.Once());

            actionResult.Should().BeOfType<OkResult>();
        }
    }
}
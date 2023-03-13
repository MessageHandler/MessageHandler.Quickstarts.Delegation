using MessageHandler.Quickstart.Contract;
using Moq;
using System.Threading.Tasks;
using Worker;
using Xunit;

namespace ComponentTests
{
    public class WhenDelegatingNotifyBuyer
    {
        [Fact]
        public async Task GivenNotifyBuyer_WhenDelegatingCommand_ShouldSendAnEmail()
        {
            // given
            var command = new NotifyBuyerCommandBuilder()
                                       .WellknownCommand("19c05669-8ee3-4d06-b2ad-94d301d7b1c4")
                                       .Build();

            // mock email
            var mockEmailSender = new Mock<ISendEmails>();
            mockEmailSender.Setup(_ => _.SendAsync("seller@seller.com", "buyer@buyer.com", "Your purchase order", "Your purchase order has been submitted for confirmation")).Verifiable();

            //when
            var reaction = new SendNotificationMail(mockEmailSender.Object);
            await reaction.Handle(command, null!);

            // Then
            mockEmailSender.Verify();
        }
    }
}
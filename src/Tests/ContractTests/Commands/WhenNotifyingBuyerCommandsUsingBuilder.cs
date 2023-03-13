using MessageHandler.Quickstart.Contract;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace MessageHandler.Quickstart.AggregateRoot.ContractTests
{
    public class WhenNotifyingBuyerCommandsUsingBuilder
    {
        [Fact]
        public async Task ShouldAdhereToContract()
        {
            var command = new NotifyBuyerCommandBuilder()
                                        .WellknownCommand("19c05669-8ee3-4d06-b2ad-94d301d7b1c4")
                                        .Build();

            string currentOutput = JsonSerializer.Serialize(command);

            await File.WriteAllTextAsync(@"./.verification/19c05669-8ee3-4d06-b2ad-94d301d7b1c4/actual.notifybuyer.command.cs.json", currentOutput);

            var previousOutput = await File.ReadAllTextAsync(@"./.verification/19c05669-8ee3-4d06-b2ad-94d301d7b1c4/verified.notifybuyer.command.cs.json");

            Assert.Equal(previousOutput, currentOutput);
        }
    }
}
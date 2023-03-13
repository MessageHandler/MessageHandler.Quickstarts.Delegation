namespace MessageHandler.Quickstart.Contract
{
    public class NotifyBuyerCommandBuilder
    {
        private NotifyBuyer _command;

        public NotifyBuyerCommandBuilder()
        {
            _command = new NotifyBuyer
            {
               
            };
        }

        public NotifyBuyerCommandBuilder WellknownCommand(string commandId)
        {
            if (_wellknownCommands.ContainsKey(commandId))
            {
                _command = _wellknownCommands[commandId]();
            }

            return this;
        }

        public NotifyBuyer Build()
        {
            return _command;
        }

        private readonly Dictionary<string, Func<NotifyBuyer>> _wellknownCommands = new()
        {
            {
                "19c05669-8ee3-4d06-b2ad-94d301d7b1c4",
                () =>
                {
                    return new NotifyBuyer()
                    {
                        From = "seller@seller.com",
                        To = "buyer@buyer.com",
                        Subject = "Your purchase order",
                        Body = "Your purchase order has been submitted for confirmation"
                    };
                }
            }
        };
    }
}
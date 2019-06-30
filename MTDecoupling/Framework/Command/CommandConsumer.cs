using MassTransit;
using System.Threading.Tasks;

namespace MTDecoupling.Framework.Command
{
    public class CommandConsumer<T> : IConsumer<T> where T : class
    {
        private readonly ICommandHandler<T> _commandHandler;

        public CommandConsumer(ICommandHandler<T> commandHandler)
        {
            _commandHandler = commandHandler;
        }

        public async Task Consume(ConsumeContext<T> context)
        {
            await _commandHandler.Handle(context.Message);
        }
    }
}

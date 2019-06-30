using Microsoft.Extensions.Logging;
using MTDecoupling.Framework.Command;
using MTDecoupling.Framework.Contract;
using System.Threading;
using System.Threading.Tasks;

namespace MTDecoupling.Framework.CommandHandlers
{
    public class EmailHandler : ICommandHandler<IEmail>
    {
        private ILogger<EmailHandler> _logger;

        public EmailHandler(ILogger<EmailHandler> logger)
        {
            _logger = logger;
        }
        public async Task Handle(IEmail message, CancellationToken token = default(CancellationToken))
        {
            _logger.LogInformation(message.Name);

            await Task.CompletedTask;
        }
    }
}

using System.Threading;
using System.Threading.Tasks;

namespace MTDecoupling.Framework.Command
{
    public interface ICommandHandler<in T>
    {
        Task Handle(T message, CancellationToken token = default(CancellationToken));
    }
}

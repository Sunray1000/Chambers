using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chambers.Commands
{
    public interface ICommand<T, TV>
    {
        T Request { get; set; }
        Task<TV> ExecuteAsync(CancellationToken cancellationToken);
        Task<TV> ExecuteAsync(T request,CancellationToken cancellationToken);
    }
}
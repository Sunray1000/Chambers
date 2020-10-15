using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Chambers.Commands
{
    public abstract class Command<T, TV> : ICommand<T, TV>
    {
        public T Request { get; set; }

        protected string RequestingIp { get; set; }
        protected string RequestingUsername { get; set; }
        public abstract Task<TV> ExecuteAsync(CancellationToken cancellationToken);
        public async Task<TV> ExecuteAsync(T request,CancellationToken cancellationToken)
        {
            Request = request;
            return await ExecuteAsync(cancellationToken);
        }
    }
}

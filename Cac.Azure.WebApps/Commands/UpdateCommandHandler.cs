using Cac.Azure.WebApps.Commands.Operations;
using Cac.Azure.WebApps.Utilities;
using Cac.Extensibility;
using Cac.Output;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cac.Azure.WebApps.Commands
{
    public abstract class UpdateCommandHandler<T> : CacCommandHandler<T> where T : UpdateCommand
    {
        protected override Task OnHandleAsync(T command, IExecutionContext context)
        {
            var webapp = new AzureWebapp(command.Webapp, command.ServicePrincipal);
            var settings = command.Operations.Where(x => x is not DeleteOperation).ToDictionary(x => x.Key, x => x.Value);
            var keysToDelete = command.Operations.Where(x => x is DeleteOperation).Select(x => x.Key).ToList();
            return OnUpdatingAsync(command, webapp, settings, keysToDelete, context.Out);
        }

        protected internal abstract Task OnUpdatingAsync(T command, AzureWebapp webapp, Dictionary<string, string> settings, List<string> keysToDelete, IOutput output);
    }
}

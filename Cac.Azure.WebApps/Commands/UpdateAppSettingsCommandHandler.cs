using Cac.Azure.WebApps.Commands.Operations;
using Cac.Azure.WebApps.Utilities;
using Cac.Extensibility;
using System.Linq;
using System.Threading.Tasks;

namespace Cac.Azure.WebApps.Commands
{
    public class UpdateAppSettingsCommandHandler : CacCommandHandler<UpdateAppSettingsCommand>
    {
        protected override Task OnHandleAsync(UpdateAppSettingsCommand command, IExecutionContext context)
        {
            var webapp = new AzureWebapp(command.Webapp, command.ServicePrincipal);
            var settings = command.Operations.Where(x => x is not DeleteOperation).ToDictionary(x => x.Key, x => x.Value);
            var keysToDelete = command.Operations.Where(x => x is DeleteOperation).Select(x => x.Key).ToList();

            context.Out.Write("Updating App settings in ");
            context.Out.WriteLine(command.Webapp.Name, System.ConsoleColor.White);

            webapp.UpdateAppSettings(settings, keysToDelete);
            return Task.CompletedTask;
        }
    }
}

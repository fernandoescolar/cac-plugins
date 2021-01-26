using Cac.Azure.WebApps.Utilities;
using Cac.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cac.Azure.WebApps.Commands
{
    public class UpdateAppSettingsCommandHandler : UpdateCommandHandler<UpdateAppSettingsCommand>
    {
        public UpdateAppSettingsCommandHandler()
        {
        }

        protected internal override Task OnUpdatingAsync(UpdateAppSettingsCommand command, AzureWebapp webapp, Dictionary<string, string> settings, List<string> keysToDelete, IOutput output)
        {
            output.Write("Updating App settings in ");
            output.WriteLine(command.Webapp.Name, System.ConsoleColor.White);

            webapp.UpdateAppSettings(settings, keysToDelete);

            return Task.CompletedTask;
        }
    }
}

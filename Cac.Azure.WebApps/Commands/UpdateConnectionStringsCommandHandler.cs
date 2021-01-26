using Cac.Azure.WebApps.Utilities;
using Cac.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cac.Azure.WebApps.Commands
{
    public class UpdateConnectionStringsCommandHandler : UpdateCommandHandler<UpdateConnectionStringsCommand>
    {
        public UpdateConnectionStringsCommandHandler()
        {
        }

        protected internal override Task OnUpdatingAsync(UpdateConnectionStringsCommand command, AzureWebapp webapp, Dictionary<string, string> settings, List<string> keysToDelete, IOutput output)
        {
            output.Write("Updating Connection strings in ");
            output.WriteLine(command.Webapp.Name, System.ConsoleColor.White);

            webapp.UpdateConnectionStrings(settings, keysToDelete);

            return Task.CompletedTask;
        }
    }
}

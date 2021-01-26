using Cac.Output;
using System.Linq;

namespace Cac.Azure.WebApps.Commands
{
    public class UpdateAppSettingsCommand : UpdateCommand
    {
        public UpdateAppSettingsCommand()
        {
        }

        protected override void OnWritePlan(IOutput output) => output.Write("App settings to update in ");
    }
}

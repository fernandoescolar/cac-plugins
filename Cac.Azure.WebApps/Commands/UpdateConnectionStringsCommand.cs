using Cac.Output;

namespace Cac.Azure.WebApps.Commands
{
    public class UpdateConnectionStringsCommand : UpdateCommand
    {
        public UpdateConnectionStringsCommand()
        {
        }

        protected override void OnWritePlan(IOutput output) => output.Write("Connection strings to update in ");
    }
}

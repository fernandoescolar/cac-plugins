using Cac.Azure.WebApps.Commands.Operations;
using Cac.Azure.WebApps.Model;
using Cac.Extensibility;
using Cac.Output;
using System.Collections.Generic;
using System.Linq;

namespace Cac.Azure.WebApps.Commands
{
    public class UpdateAppSettingsCommand : ICacCommand
    {
        public Webapp Webapp { get; set;  }

        public ServicePrincipal ServicePrincipal { get; set; }

        public IEnumerable<IOperation> Operations { get; set; }

        internal UpdateAppSettingsCommand()
        { 
        }

        public void WritePlan(IOutput output)
        {
            output.Write("App settings to update in ");
            output.WriteLine(Webapp.Name, System.ConsoleColor.White);
            output.BeginSection();
            Operations.ToList().ForEach(x => x.WritePlan(output));
            output.EndSection();
        }
    }
}

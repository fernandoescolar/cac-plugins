using Cac.Azure.WebApps.Commands.Operations;
using Cac.Azure.WebApps.Model;
using Cac.Extensibility;
using Cac.Output;
using System.Collections.Generic;
using System.Linq;

namespace Cac.Azure.WebApps.Commands
{
    public abstract class UpdateCommand : ICacCommand
    {
        public Webapp Webapp { get; set;  }

        public ServicePrincipal ServicePrincipal { get; set; }

        public IEnumerable<IOperation> Operations { get; set; }

        public void WritePlan(IOutput output)
        {
            OnWritePlan(output);
            output.WriteLine(Webapp.Name, System.ConsoleColor.White);
            output.BeginSection();
            Operations.ToList().ForEach(x => x.WritePlan(output));
            output.EndSection();
        }

        protected abstract void OnWritePlan(IOutput output);
    }
}

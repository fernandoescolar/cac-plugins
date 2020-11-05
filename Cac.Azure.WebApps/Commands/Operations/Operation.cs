using Cac.Output;

namespace Cac.Azure.WebApps.Commands.Operations
{
    public abstract class Operation : IOperation
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public abstract void WritePlan(IOutput output);
    }
}

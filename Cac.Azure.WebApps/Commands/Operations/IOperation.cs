using Cac.Output;

namespace Cac.Azure.WebApps.Commands.Operations
{
    public interface IOperation
    {
        string Key { get; set; }
        string Value { get; set; }
        void WritePlan(IOutput output);
    }
}

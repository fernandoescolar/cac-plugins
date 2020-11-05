using System;
using Cac.Output;

namespace Cac.Azure.WebApps.Commands.Operations
{
    public class DeleteOperation : Operation
    {
        public override void WritePlan(IOutput output)
        {
            output.Write(Key, ConsoleColor.White);
            output.Write(" => ");
            output.Write(Value, ConsoleColor.Red, ConsoleColor.DarkRed);
            output.WriteLine(string.Empty);
        }
    }
}

using System;
using Cac.Output;

namespace Cac.Azure.WebApps.Commands.Operations
{
    public class AddOperation : Operation
    {
        public override void WritePlan(IOutput output)
        {
            output.Write(Key, ConsoleColor.White);
            output.Write(" => ");
            output.Write(Value, ConsoleColor.Green, ConsoleColor.DarkGreen);
            output.WriteLine(string.Empty);
        }
    }
}

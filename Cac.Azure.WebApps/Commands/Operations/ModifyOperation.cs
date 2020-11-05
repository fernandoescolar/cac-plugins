using Cac.Output;
using System;

namespace Cac.Azure.WebApps.Commands.Operations
{
    public class ModifyOperation : Operation
    {
        public string OldValue { get; set; }

        public override void WritePlan(IOutput output)
        {
            output.Write(Key, ConsoleColor.White);
            output.Write(" => ");
            output.Write(Value, ConsoleColor.Green, ConsoleColor.DarkGreen);
            output.WriteLine(string.Empty);
            output.Write(new string(' ', Key.Length + 4));
            output.Write(OldValue, ConsoleColor.Red, ConsoleColor.DarkRed);
            output.WriteLine(string.Empty);
        }
    }
}

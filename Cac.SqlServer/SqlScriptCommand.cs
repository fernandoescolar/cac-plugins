using Cac.Extensibility;
using Cac.Output;
using Microsoft.Data.SqlClient;

namespace Cac.SqlServer
{
    public class SqlScriptCommand : ICacCommand
    {
        public string ConnectionString { get; set; }

        public string[] Statements { get; set; }

        public void WritePlan(IOutput output)
        {
            var c = new SqlConnectionStringBuilder(ConnectionString);
            output.WriteLine($"sql statement: {c.DataSource}");
            output.BeginSection();
            foreach (var s in Statements)
            {
                output.Verbose.WriteLine(s);
            }
            output.EndSection();
        }
    }
}

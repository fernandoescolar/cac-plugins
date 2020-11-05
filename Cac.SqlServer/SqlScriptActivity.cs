using Cac.Extensibility;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cac.SqlServer
{
    [Name("sqlserver_script")]
    public class SqlScriptActivity : CacActivity<SqlServerModel>
    {
        protected override Task<IEnumerable<ICacCommand>> OnPlanAsync(SqlServerModel model, IExecutionContext context)
        {
            context.Out.WriteLine($"testing sql statements");
            SqlHelper.TestStatements(model.ConnectionString, model.Content);

            var command = new SqlScriptCommand { ConnectionString = model.ConnectionString, Statements = model.Content.ToArray() };
            return Task.FromResult<IEnumerable<ICacCommand>>(new ICacCommand[] { command });
        }       
    }
}

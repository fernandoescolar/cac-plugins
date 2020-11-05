using Cac.Extensibility;
using System.Threading.Tasks;

namespace Cac.SqlServer
{
    public class SqlScriptCommandHandler : CacCommandHandler<SqlScriptCommand>
    {
        protected override Task OnHandleAsync(SqlScriptCommand command, IExecutionContext context)
        {
            SqlHelper.RunStatements(command.ConnectionString, command.Statements);
            return Task.CompletedTask;
        }
    }
}

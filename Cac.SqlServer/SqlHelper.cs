using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Cac.SqlServer
{
    internal static class SqlHelper
    {
        public static void TestStatements(string connectionString, IEnumerable<string> statements)
        {
            RunStatements(connectionString, statements, false);
        }

        public static void RunStatements(string connectionString, IEnumerable<string> statements, bool commit = true)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var statement in statements)
                    {
                        using (var command = transaction.Connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandText = statement;
                            command.ExecuteNonQuery();
                        }
                    }

                    if (commit)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}

using Cac.Yaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Cac.SqlServer
{
    public class SqlServerModel
    {
        private readonly Lazy<IEnumerable<string>> _contentAccessor;

        public SqlServerModel()
        {
            _contentAccessor = new Lazy<IEnumerable<string>>(() => GetContent());
        }

        [YamlProperty("connection_string", IsRequired = true)]
        public string ConnectionString { get; set; }

        [YamlProperty("sql")]
        public string SqlStatement { get; set; }

        [YamlProperty("file")]
        public string ScriptFilepath { get; set; }

        internal IEnumerable<string> Content => _contentAccessor.Value;

        private IEnumerable<string> GetContent()
        {
            var content = !string.IsNullOrWhiteSpace(SqlStatement) ? SqlStatement : ReadTextFromFile(ScriptFilepath);
            return SplitSqlStatements(content);
        }

        private static string ReadTextFromFile(string filepath)
        {
            if (!File.Exists(filepath)) throw new FileNotFoundException($"Sql file nor found: ${filepath}");
            return File.ReadAllText(filepath);
        }

        private static IEnumerable<string> SplitSqlStatements(string sqlScript)
        {
            var statements = Regex.Split(
                    sqlScript,
                    @"^[\t ]*GO[\t ]*\d*[\t ]*(?:--.*)?$",
                    RegexOptions.Multiline |
                    RegexOptions.IgnorePatternWhitespace |
                    RegexOptions.IgnoreCase);

            return statements
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .Select(x => x.Trim(' ', '\r', '\n'));
        }
    }
}

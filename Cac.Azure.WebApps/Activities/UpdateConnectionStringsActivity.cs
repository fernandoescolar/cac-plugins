using Cac.Azure.WebApps.Commands;
using Cac.Azure.WebApps.Model;
using Cac.Extensibility;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Cac.Azure.WebApps.Activities
{
    [Name("azure_webapp_update_connection_strings")]
    public class UpdateConnectionStringsActivity : CacActivity<UpdateModel>
    {
        protected override Task<IEnumerable<ICacCommand>> OnPlanAsync(UpdateModel model, IExecutionContext context)
        {
            var settings = model.Settings ?? GetSettings(model.SettingsFilepath) ?? new Dictionary<string, string>();
            var command = UpdateCommandFactory.CreateConnectionStringsCommand(model.Webapp, model.ServicePrincipal, settings, model.IncludeDeletion);

            return Task.FromResult<IEnumerable<ICacCommand>>(new ICacCommand[] { command });
        }

        private static Dictionary<string, string> GetSettings(string file)
        {
            if (!File.Exists(file)) throw new FileNotFoundException($"File not found: {file}");

            var json = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}

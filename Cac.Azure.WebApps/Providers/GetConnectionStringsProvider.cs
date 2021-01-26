using Cac.Azure.WebApps.Model;
using Cac.Azure.WebApps.Utilities;
using Cac.Extensibility;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cac.Azure.WebApps.Providers
{
    [Name("azure_webapp_connection_strings")]
    public class GetConnectionStringsProvider : CacProvider<GetModel, Dictionary<string, string>>
    {
        protected override Task<Dictionary<string, string>> OnGetValueAsync(GetModel model, IExecutionContext context)
        {
            var azure = new AzureWebapp(model.Webapp, model.ServicePrincipal);
            return azure.GetConnectionStringsAsync();
        }
    }
}

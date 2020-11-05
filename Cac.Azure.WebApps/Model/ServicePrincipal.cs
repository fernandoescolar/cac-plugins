using Cac.Yaml;

namespace Cac.Azure.WebApps.Model
{
    public class ServicePrincipal
    {
        [YamlProperty("tenant_id", IsRequired = true)]
        public string TenantId { get; set; }

        [YamlProperty("client_id", IsRequired = true)]
        public string ClientId { get; set; }

        [YamlProperty("client_secret", IsRequired = true)]  
        public string ClientSecret { get; set; }
    }
}

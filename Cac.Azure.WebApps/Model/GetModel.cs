using Cac.Yaml;

namespace Cac.Azure.WebApps.Model
{
    public class GetModel 
    {
        [YamlProperty("webapp", IsRequired = true)]
        public Webapp Webapp { get; set; }

        [YamlProperty("service_principal", IsRequired = true)]
        public ServicePrincipal ServicePrincipal { get; set; }
    }
}

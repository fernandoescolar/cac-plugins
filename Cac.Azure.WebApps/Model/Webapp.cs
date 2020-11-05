using Cac.Yaml;

namespace Cac.Azure.WebApps.Model
{
    public class Webapp
    {
        [YamlProperty("name", IsRequired = true)]
        public string Name { get; set; }

        [YamlProperty("resource_group", IsRequired = true)]
        public string ResourceGroup { get; set; }

        [YamlProperty("slot")]
        public string Slot { get; set; }

        [YamlProperty("subscription_id", IsRequired = true)]
        public string SubscriptionId { get; set; }

        public string AppResourceId => $"subscriptions/{SubscriptionId}/resourceGroups/{ResourceGroup}/providers/Microsoft.Web/sites/{Name}{SlotResourcePath}";

        private string SlotResourcePath => !string.IsNullOrWhiteSpace(Slot) ? $"/slots/{Slot}" : string.Empty;
    }
}

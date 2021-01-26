using Cac.Yaml;
using System.Collections.Generic;

namespace Cac.Azure.WebApps.Model
{
    public class UpdateModel : GetModel
    {
        [YamlProperty("settings")]
        public Dictionary<string, string> Settings { get; set; }

        [YamlProperty("settings_file")]
        public string SettingsFilepath { get; set; }

        [YamlProperty("include_deletion")]
        public bool IncludeDeletion { get; set; }
    }
}

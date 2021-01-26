using Cac.Azure.WebApps.Commands.Operations;
using Cac.Azure.WebApps.Model;
using Cac.Azure.WebApps.Utilities;
using System.Collections.Generic;

namespace Cac.Azure.WebApps.Commands
{
    internal class UpdateCommandFactory 
    {
        public static UpdateAppSettingsCommand CreateAppSettingsCommand(Webapp target, ServicePrincipal servicePrincipal, IDictionary<string, string> requestedSettings, bool includeDeletion)
        {
            var webapp = new AzureWebapp(target, servicePrincipal);
            var settingsInTarget = webapp.GetAppSettings();
            var operations = GetOperations(requestedSettings, settingsInTarget, includeDeletion);
            return new UpdateAppSettingsCommand { Webapp = target, ServicePrincipal = servicePrincipal, Operations = operations };
        }

        public static UpdateConnectionStringsCommand CreateConnectionStringsCommand(Webapp target, ServicePrincipal servicePrincipal, IDictionary<string, string> requestedSettings, bool includeDeletion)
        {
            var webapp = new AzureWebapp(target, servicePrincipal);
            var settingsInTarget = webapp.GetConnectionStrings();
            var operations = GetOperations(requestedSettings, settingsInTarget, includeDeletion);
            return new UpdateConnectionStringsCommand { Webapp = target, ServicePrincipal = servicePrincipal, Operations = operations };
        }

        private static IEnumerable<IOperation> GetOperations(IDictionary<string, string> requestedSettings, IDictionary<string, string> settingsInTarget, bool includeDeletion)
        {
            foreach (var setting in requestedSettings)
            {
                if (settingsInTarget.ContainsKey(setting.Key))
                {
                    if (settingsInTarget[setting.Key] == setting.Value)
                    {
                        continue;
                    }
                    else
                    {
                        yield return new ModifyOperation { Key = setting.Key, Value = setting.Value, OldValue = settingsInTarget[setting.Key] };
                    }
                }
                else
                {
                    yield return new AddOperation { Key = setting.Key, Value = setting.Value };
                }
            }

            if (!includeDeletion)
                yield break;

            foreach (var setting in settingsInTarget)
            {
                if (!requestedSettings.ContainsKey(setting.Key))
                {
                    yield return new DeleteOperation { Key = setting.Key, Value = setting.Value };
                }
            }
        }
    }
}

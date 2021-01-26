using Cac.Azure.WebApps.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cac.Azure.WebApps.Utilities
{
    public class AzureWebapp
    {
        private readonly Webapp _target;
        private readonly ServicePrincipal _servicePrincipal;

        public AzureWebapp(Webapp target, ServicePrincipal servicePrincipal)
        {
            _target = target;
            _servicePrincipal = servicePrincipal;
        }

        public Dictionary<string, string> GetAppSettings()
        {
            var task = ListAppSettingsAsync(_target);
            task.Wait();
            return task.Result;
        }

        public Dictionary<string, string> GetConnectionStrings()
        {
            var task = ListConnectionStringsAsync(_target);
            task.Wait();
            return task.Result;
        }

        public Task<Dictionary<string, string>> GetAppSettingsAsync()
        {
            return ListAppSettingsAsync(_target);
        }

        public Task<Dictionary<string, string>> GetConnectionStringsAsync()
        {
            return ListConnectionStringsAsync(_target);
        }

        public void UpdateAppSettings(Dictionary<string, string> settings, IEnumerable<string> keysToDelete)
        {
            var appsettings = GetAppSettings();
            foreach (var s in settings)
            {
                appsettings[s.Key] = s.Value;
            }

            foreach (var key in keysToDelete)
            {
                if (appsettings.ContainsKey(key))
                    appsettings.Remove(key);
            }

            var task = UpdateAppSettingsAsync(_target, appsettings);
            task.Wait();
        }

        public void UpdateConnectionStrings(Dictionary<string, string> newValues, IEnumerable<string> keysToDelete)
        {
            var connectionStrings = GetConnectionStrings();
            foreach (var s in newValues)
            {
                connectionStrings[s.Key] = s.Value;
            }

            foreach (var key in keysToDelete)
            {
                if (connectionStrings.ContainsKey(key))
                    connectionStrings.Remove(key);
            }

            var task = UpdateConnectionStringsAsync(_target, connectionStrings);
            task.Wait();
        }

        private Task<Dictionary<string, string>> ListAppSettingsAsync(Webapp webapp)
        {
            var url = $"https://management.azure.com/{webapp.AppResourceId}/config/appsettings/list?api-version=2019-08-01";
            return ListAsync(url);
        }

        private Task UpdateAppSettingsAsync(Webapp webapp, Dictionary<string, string> settings)
        {
            var url = $"https://management.azure.com/{webapp.AppResourceId}/config/appsettings?api-version=2019-08-01";
            return UpdateAsync(url, settings);
        }

        private Task<Dictionary<string, string>> ListConnectionStringsAsync(Webapp webapp)
        {
            var url = $"https://management.azure.com/{webapp.AppResourceId}/config/connectionstrings/list?api-version=2019-08-01";
            return ListAsync(url);
        }

        private Task UpdateConnectionStringsAsync(Webapp webapp, Dictionary<string, string> connectionStrings)
        {
            var url = $"https://management.azure.com/{webapp.AppResourceId}/config/connectionstrings?api-version=2019-08-01";
            return UpdateAsync(url, connectionStrings);
        }

        private async Task<Dictionary<string, string>> ListAsync(string url)
        {
            var auth = await GetAuthAsync();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(auth.Type, auth.AccessToken);
                var response = await client.PostAsync(url, new StringContent(string.Empty));
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Result>(json);
                return result.Properties;
            }
        }

        private async Task UpdateAsync(string url, Dictionary<string, string> valueTypePairList)
        {
            var auth = await GetAuthAsync();
            var data = new Dictionary<string, object>
            {
                { "kind", "app" },
                { "properties", valueTypePairList }
            };
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(auth.Type, auth.AccessToken);
                await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            }
        }

        private Task<AuthResponse> GetAuthAsync()
        {
            return GetAuthAsync(_servicePrincipal.TenantId, _servicePrincipal.ClientId, _servicePrincipal.ClientSecret);
        }

        private async Task<AuthResponse> GetAuthAsync(string tenantId, string clientId, string clientSecret)
        {
            var url = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token";
            var data = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "scope", "https://management.core.windows.net/.default" }
            };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsync(url, new FormUrlEncodedContent(data));
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AuthResponse>(json);
            }
        }

        class AuthResponse
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("token_type")]
            public string Type { get; set; }

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }

            [JsonProperty("ext_expires_in")]
            public int ExtExpiresIn { get; set; }

            public AuthenticationHeaderValue AsHeader()
            {
                return new AuthenticationHeaderValue(Type, AccessToken);
            }
        }

        public class Result
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("location")]
            public string Location { get; set; }

            [JsonProperty("tags")]
            public object Tags { get; set; }

            [JsonProperty("properties")]
            public Dictionary<string, string> Properties { get; set; }
        }
    }
}

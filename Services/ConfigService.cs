using System.Text.RegularExpressions;

namespace TGWHomeTask.Services
{
    public class ConfigService : IConfigService
    {
        IDictionary<string, string> _configs;

        const string PATTERN = @"[a-zA-Z]+:\t+.*\t";

        // Constructor ------------------------------------------------------------------------------------
        public ConfigService(IDictionary<string, string> configs)
        {
            _configs = configs;
        }

        // -------------------------------------------------------------------------------------------------
        public void ParseConfigText(string configText)
        {
            foreach (Match match in Regex.Matches(configText, PATTERN))
            {
                var keyValue = match.Value.Split(":\t");
                string key = keyValue[0].Trim().ToLower();
                string value = keyValue[1].Trim();

                if (_configs.ContainsKey(key))
                {
                    _configs[key] = value;
                }
                else
                {
                    _configs.Add(key, value);
                }
            }
        }

        // --------------------------------------------------------------------------------------------------
        public IEnumerable<string> GetAllConfigNames()
        {
            return _configs.Keys;
        }

        // --------------------------------------------------------------------------------------------------
        public string GetConfig(string configName)
        {
            string configNameLower = configName.ToLower();
            if (!_configs.ContainsKey(configNameLower))
            {
                throw new KeyNotFoundException($"{configName} does not exist");
            }

            return _configs[configNameLower];
        }
    }
}
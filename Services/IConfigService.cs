namespace TGWHomeTask.Services
{
    public interface IConfigService
    {
        void ParseConfigText(string configText);

        IEnumerable<string> GetAllConfigNames();

        string GetConfig(string configName);
    }
}
namespace Agape316.Helpers
{
    public class ConfigHelper
    {

        private static ConfigHelper _appSettings;

        public string appSettingValue { get; set; }

        public static string AppSetting(string Key)
        {
            _appSettings = GetCurrentSettings(Key);
            return _appSettings.appSettingValue;
        }

        public ConfigHelper(IConfiguration config, string Key)
        {
            this.appSettingValue = config.GetValue<string>(Key);
        }

        public static ConfigHelper GetCurrentSettings(string Key)
        {            
#if DEBUG
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
#else
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Production.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
#endif

            IConfigurationRoot configuration = builder.Build();

            var settings = new ConfigHelper(configuration.GetSection("ConnectionStrings"), Key);

            return settings;
        }
    }
}

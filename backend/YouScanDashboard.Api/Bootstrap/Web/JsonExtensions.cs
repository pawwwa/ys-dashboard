using System.Text.Json.Serialization;

namespace YouScanDashboard.Api.Bootstrap.Web;

public static class JsonExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public void AddCustomJsonSettings()
        {
            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        }
    }
}

using YouScanDashboard.Api.Bootstrap.Validation;
using YouScanDashboard.Api.Bootstrap.Web;

namespace YouScanDashboard.Api.Bootstrap;

public static class WebApplicationBootstrapExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder AddWebDependencies()
        {
            builder.AddFluentValidation();
            builder.AddCorsPolicies();
            builder.AddCustomJsonSettings();

            return builder;
        }
    }

    extension(WebApplication application)
    {
        public WebApplication UseWebDependencies()
        {
            application.UseCorsPolicies();

            return application;
        }
    }
}

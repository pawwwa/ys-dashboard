namespace YouScanDashboard.Api.Bootstrap.Web;

public static class CorsExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public void AddCorsPolicies()
        {
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }
    }

    extension(WebApplication application)
    {
        public void UseCorsPolicies()
        {
            application.UseCors();
        }
    }
}

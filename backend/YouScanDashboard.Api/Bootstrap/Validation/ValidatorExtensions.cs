using FluentValidation;
using System.Reflection;

namespace YouScanDashboard.Api.Bootstrap.Validation;

public static class ValidatorExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public void AddFluentValidation()
        {
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

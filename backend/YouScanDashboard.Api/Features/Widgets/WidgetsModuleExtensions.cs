using Microsoft.AspNetCore.Mvc;
using YouScanDashboard.Api.Common.Validation;
using YouScanDashboard.Api.Features.Widgets.Handlers;
using YouScanDashboard.Api.Features.Widgets.Requests;

namespace YouScanDashboard.Api.Features.Widgets;

public static class WidgetsModuleExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder AddWidgetsModule()
        {
            builder.Services.AddScoped<ListWidgetsHandler>();
            builder.Services.AddScoped<GetWidgetHandler>();
            builder.Services.AddScoped<CreateWidgetHandler>();
            builder.Services.AddScoped<UpdateTextWidgetHandler>();
            builder.Services.AddScoped<DeleteWidgetHandler>();

            return builder;
        }
    }

    extension(WebApplication application)
    {
        public WebApplication UseWidgetsModule()
        {
            var widgetsGroup = application.MapGroup("/api/widgets")
                .WithTags("Widgets");

            widgetsGroup.MapGet("/", async (ListWidgetsHandler handler, CancellationToken ct)
                => await handler.HandleAsync(ct));

            widgetsGroup.MapGet("/{id:guid:required}", async (GetWidgetHandler handler, Guid id, CancellationToken ct)
                => await handler.HandleAsync(id, ct));

            widgetsGroup.MapPost("/", async (
                    CreateWidgetHandler handler,
                    [FromBody] CreateWidgetRequest request,
                    CancellationToken ct)
                => await handler.HandleAsync(request, ct))
                .AddEndpointFilter<ValidationFilter<CreateWidgetRequest>>();

            widgetsGroup.MapPut("/{id:guid:required}/text", async (
                    UpdateTextWidgetHandler handler,
                    Guid id,
                    [FromBody] UpdateTextWidgetRequest request,
                    CancellationToken ct)
                => await handler.HandleAsync(id, request, ct))
                .AddEndpointFilter<ValidationFilter<UpdateTextWidgetRequest>>();

            widgetsGroup.MapDelete("/{id:guid}", async (DeleteWidgetHandler handler, Guid id, CancellationToken ct)
                => await handler.HandleAsync(id, ct));

            return application;
        }
    }
}

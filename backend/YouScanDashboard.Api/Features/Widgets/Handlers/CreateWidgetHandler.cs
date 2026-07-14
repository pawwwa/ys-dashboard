using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using YouScanDashboard.Api.Database;
using YouScanDashboard.Api.Database.Entities;
using YouScanDashboard.Api.Features.Widgets.Dtos;
using YouScanDashboard.Api.Features.Widgets.Requests;

namespace YouScanDashboard.Api.Features.Widgets.Handlers;

public class CreateWidgetHandler(AppDbContext context)
{
    private static readonly string[] ChartLabels = ["Jan", "Feb", "Mar", "Apr", "May", "Jun"];

    public async Task<Created<WidgetDto>> HandleAsync(CreateWidgetRequest request, CancellationToken ct)
    {
        var position = await context.Widgets.CountAsync(ct);

        var entity = new Widget
        {
            Id = Guid.NewGuid(),
            Type = request.Type,
            Position = position,
            Text = CreateText(request),
            Points = CreatePoints(request.Type)
        };

        context.Widgets.Add(entity);
        await context.SaveChangesAsync(ct);

        return TypedResults.Created($"/api/widgets/{entity.Id}", WidgetMapper.ToDto(entity));
    }

    private static string? CreateText(CreateWidgetRequest request)
    {
        if (request.Type != WidgetType.Text)
        {
            return null;
        }

        return string.IsNullOrWhiteSpace(request.Text)
            ? "Click Edit to change this text."
            : request.Text;
    }

    private static List<ChartPoint>? CreatePoints(WidgetType type)
    {
        if (type is not (WidgetType.LineChart or WidgetType.BarChart))
        {
            return null;
        }

        return ChartLabels
            .Select((label, order) => new ChartPoint
            {
                Order = order,
                Label = label,
                Value = Random.Shared.Next(10, 90)
            })
            .ToList();
    }
}

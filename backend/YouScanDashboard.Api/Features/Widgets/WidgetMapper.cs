using YouScanDashboard.Api.Database.Entities;
using YouScanDashboard.Api.Features.Widgets.Dtos;

namespace YouScanDashboard.Api.Features.Widgets;

public static class WidgetMapper
{
    public static WidgetDto ToDto(Widget widget) => new(
        widget.Id,
        widget.Type,
        widget.Position,
        widget.Text,
        widget.Points?
            .OrderBy(p => p.Order)
            .Select(p => new ChartPointDto(p.Order, p.Label, p.Value))
            .ToList(),
        widget.CreatedAt,
        widget.UpdatedAt);
}

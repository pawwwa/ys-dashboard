using YouScanDashboard.Api.Database.Entities;

namespace YouScanDashboard.Api.Features.Widgets.Dtos;

public record ChartPointDto(int Order, string Label, int Value);

public record WidgetDto(
    Guid Id,
    WidgetType Type,
    int Position,
    string? Text,
    IReadOnlyList<ChartPointDto>? Points,
    DateTime CreatedAt,
    DateTime? UpdatedAt);

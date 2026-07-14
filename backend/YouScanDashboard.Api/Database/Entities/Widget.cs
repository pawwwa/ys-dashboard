namespace YouScanDashboard.Api.Database.Entities;

public enum WidgetType { Text, LineChart, BarChart }

public class ChartPoint
{
    public required int Order { get; set; }
    public required string Label { get; set; }
    public required int Value { get; set; }
}

public class Widget
{
    public Guid Id { get; set; }
    public required WidgetType Type { get; init; }
    public required int Position { get; set; }

    public string? Text { get; set; }
    public List<ChartPoint>? Points { get; set; }
    
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
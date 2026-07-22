using FluentValidation;
using YouScanDashboard.Api.Database.Entities;

namespace YouScanDashboard.Api.Features.Widgets.Requests;

public class CreateWidgetRequest
{
    public required WidgetType Type { get; set; }
    public string? Text { get; set; }
    public int Rows { get; set; } = 1;
    public int Columns { get; set; } = 1;
}

public class CreateWidgetRequestValidator : AbstractValidator<CreateWidgetRequest>
{
    public CreateWidgetRequestValidator()
    {
        RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage("Type must be one of: Text, LineChart, BarChart.");

        RuleFor(x => x.Text)
            .MaximumLength(5000)
            .WithMessage("Text cannot exceed 5000 characters.");

        RuleFor(x => x.Text)
            .Empty()
            .When(x => x.Type != WidgetType.Text)
            .WithMessage("Only text widgets can include text.");
        
        RuleFor(x => x.Rows)
            .InclusiveBetween(1, 2).WithMessage("Rows must be between 1 and 2.");
        
        RuleFor(x => x.Columns)
            .InclusiveBetween(1, 3).WithMessage("Columns must be between 1 and 3.");
    }
}

using FluentValidation;
using YouScanDashboard.Api.Database.Entities;

namespace YouScanDashboard.Api.Features.Widgets.Requests;

public class CreateWidgetRequest
{
    public required WidgetType Type { get; set; }
    public string? Text { get; set; }
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
    }
}

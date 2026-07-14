using FluentValidation;

namespace YouScanDashboard.Api.Features.Widgets.Requests;

public class UpdateTextWidgetRequest
{
    public string Text { get; set; } = string.Empty;
}

public class UpdateTextWidgetRequestValidator : AbstractValidator<UpdateTextWidgetRequest>
{
    public UpdateTextWidgetRequestValidator()
    {
        RuleFor(x => x.Text)
            .NotNull()
            .MaximumLength(5000)
            .WithMessage("Text cannot exceed 5000 characters.");
    }
}

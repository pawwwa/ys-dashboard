using FluentValidation;

namespace YouScanDashboard.Api.Features.Widgets.Requests;

public class UpdateWidgetSizeRequest
{
    public required int Rows { get; set; }
    public required int Columns { get; set; }
}

public class UpdateWidgetSizeRequestValidator : AbstractValidator<UpdateWidgetSizeRequest>
{
    public UpdateWidgetSizeRequestValidator()
    {
        RuleFor(x => x.Rows)
            .InclusiveBetween(1, 2).WithMessage("Rows must be between 1 and 2.");
        
        RuleFor(x => x.Columns)
            .InclusiveBetween(1, 3).WithMessage("Columns must be between 1 and 3.");
    }
}
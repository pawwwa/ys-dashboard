using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using YouScanDashboard.Api.Database;
using YouScanDashboard.Api.Database.Entities;
using YouScanDashboard.Api.Features.Widgets.Dtos;
using YouScanDashboard.Api.Features.Widgets.Requests;

namespace YouScanDashboard.Api.Features.Widgets.Handlers;

public class UpdateTextWidgetHandler(AppDbContext context)
{
    public async Task<Results<Ok<WidgetDto>, NotFound, BadRequest<string>>> HandleAsync(
        Guid id,
        UpdateTextWidgetRequest request,
        CancellationToken ct)
    {
        var widget = await context.Widgets.FirstOrDefaultAsync(w => w.Id == id, ct);
        if (widget is null)
        {
            return TypedResults.NotFound();
        }

        if (widget.Type != WidgetType.Text)
        {
            return TypedResults.BadRequest("Only text widgets can update text.");
        }

        widget.Text = request.Text;
        widget.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(ct);

        return TypedResults.Ok(WidgetMapper.ToDto(widget));
    }
}

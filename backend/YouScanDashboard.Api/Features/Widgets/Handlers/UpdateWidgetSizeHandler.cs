using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouScanDashboard.Api.Database;
using YouScanDashboard.Api.Features.Widgets.Requests;

namespace YouScanDashboard.Api.Features.Widgets.Handlers;

public class UpdateWidgetSizeHandler(AppDbContext context)
{
    public async Task<Results<NotFound, Ok>> HandleAsync(Guid id, UpdateWidgetSizeRequest request, CancellationToken ct)
    {
        var widget = await context.Widgets
            .FirstOrDefaultAsync(w => w.Id == id, ct);
        
        if (widget is null)
        {
            return TypedResults.NotFound();
        }

        widget.Columns = request.Columns;
        widget.Rows = request.Rows;

        await context.SaveChangesAsync(ct);
        
        return TypedResults.Ok();
    }
}
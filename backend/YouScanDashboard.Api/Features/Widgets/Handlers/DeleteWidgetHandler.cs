using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using YouScanDashboard.Api.Database;

namespace YouScanDashboard.Api.Features.Widgets.Handlers;

public class DeleteWidgetHandler(AppDbContext context)
{
    public async Task<Results<NoContent, NotFound>> HandleAsync(Guid id, CancellationToken ct)
    {
        var widget = await context.Widgets.FirstOrDefaultAsync(w => w.Id == id, ct);
        if (widget is null)
        {
            return TypedResults.NotFound();
        }

        context.Widgets.Remove(widget);
        await context.SaveChangesAsync(ct);

        var remaining = await context.Widgets
            .OrderBy(w => w.Position)
            .ToListAsync(ct);

        for (var i = 0; i < remaining.Count; i++)
        {
            remaining[i].Position = i;
        }

        await context.SaveChangesAsync(ct);

        return TypedResults.NoContent();
    }
}

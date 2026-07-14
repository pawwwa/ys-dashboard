using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using YouScanDashboard.Api.Database;
using YouScanDashboard.Api.Features.Widgets.Dtos;

namespace YouScanDashboard.Api.Features.Widgets.Handlers;

public class GetWidgetHandler(AppDbContext context)
{
    public async Task<Results<Ok<WidgetDto>, NotFound>> HandleAsync(Guid id, CancellationToken ct)
    {
        var widget = await context.Widgets
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.Id == id, ct);

        if (widget is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(WidgetMapper.ToDto(widget));
    }
}

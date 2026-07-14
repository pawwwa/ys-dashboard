using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using YouScanDashboard.Api.Database;
using YouScanDashboard.Api.Features.Widgets.Dtos;

namespace YouScanDashboard.Api.Features.Widgets.Handlers;

public class ListWidgetsHandler(AppDbContext context)
{
    public async Task<Ok<IReadOnlyList<WidgetDto>>> HandleAsync(CancellationToken ct)
    {
        var widgets = await context.Widgets
            .AsNoTracking()
            .OrderBy(w => w.Position)
            .ToListAsync(ct);

        IReadOnlyList<WidgetDto> result = widgets.Select(WidgetMapper.ToDto).ToList();
        return TypedResults.Ok(result);
    }
}

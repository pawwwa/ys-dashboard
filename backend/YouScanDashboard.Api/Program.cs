using Microsoft.EntityFrameworkCore;
using YouScanDashboard.Api.Bootstrap;
using YouScanDashboard.Api.Database;
using YouScanDashboard.Api.Features.Widgets;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.AddWebDependencies();
builder.AddWidgetsModule();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseWebDependencies();
app.UseWidgetsModule();

app.UseHttpsRedirection();

app.Run();
using Food_Waste_API;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MenuDb>(opt => opt.UseInMemoryDatabase("MenuList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

app.MapGet("/menuitems", async (MenuDb db) =>
    await db.Menus.ToListAsync());

app.MapGet("/menuitems/{id}", async (int id, MenuDb db) =>
    await db.Menus.FindAsync(id)
        is Menu menu
            ? Results.Ok(menu)
            : Results.NotFound());

app.MapPost("/menuitems", async (Menu menu, MenuDb db) =>
{
    db.Menus.Add(menu);
    await db.SaveChangesAsync();

    return Results.Created($"/menuitems/{menu.Id}", menu);
});

app.MapPut("/menuitems/{id}", async (int id, Menu inputMenu, MenuDb db) =>
{
    var menu = await db.Menus.FindAsync(id);

    if (menu is null) return Results.NotFound();

    menu.Name = inputMenu.Name;
    menu.IsComplete = inputMenu.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/menuitems/{id}", async (int id, MenuDb db) =>
{
    if (await db.Menus.FindAsync(id) is Menu menu)
    {
        db.Menus.Remove(menu);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();

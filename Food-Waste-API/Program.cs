using Food_Waste_API;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text.Json;

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

SeedDatabase(app);
app.UseCors("AllowAll");
   
app.MapGet("/menus/join/{id}/{user}", async (MenuDb db, Guid id, string user) =>
{
    Menu? menu = await db.Menus.FindAsync(id);

    if (menu == null)
    {
        return Results.NotFound();
    }

    menu?.attendees.Add(user);
    await db.SaveChangesAsync();

    return Results.Ok(menu);

});

app.MapGet("/menus", async (MenuDb db) =>
{
    await db.Dishes.ToListAsync();
    await db.Ingredients.ToListAsync();
    await db.Comments.ToListAsync();

    return await db.Menus.ToListAsync();

});

app.MapGet("/menus/{id}", async (MenuDb db, Guid id) =>
{
    await db.Dishes.ToListAsync();
    await db.Ingredients.ToListAsync();
    await db.Comments.ToListAsync();

    return await db.Menus.FindAsync(id)
        is Menu menu
            ? Results.Ok(menu)
            : Results.NotFound();
});


app.MapPost("/menus", async (MenuDb db, Menu menu) =>
{
    db.Menus.Add(menu);
    await db.SaveChangesAsync();

    return Results.Created($"/menus/{menu.id}", menu);
});

app.MapPost("/menus/comment/{id}", async (MenuDb db, Guid id, Comment comment) =>
{
    Menu? menu = await db.Menus.FindAsync(id);
    await db.Comments.ToListAsync();

    if (menu == null)
    {
        return Results.NotFound();
    }

    comment.dateCreated = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
    Random rng = new();
    comment.id = rng.Next();

    menu?.comments.Add(comment);
    db.Entry(comment).State = EntityState.Added;
    await db.SaveChangesAsync();

    return Results.Ok(menu);
});

app.MapDelete("/menus/{id}", async (MenuDb db, Guid id) =>
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

void SeedDatabase(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<MenuDb>();

        if (!context.Menus.Any())
        {
            var jsonData = File.ReadAllText("menuSeedData.json");
            var menus = JsonSerializer.Deserialize<List<Menu>>(jsonData);

            if (menus != null)
            {
                context.Menus.AddRange(menus);
                context.SaveChanges();
            }
        }
    }
}
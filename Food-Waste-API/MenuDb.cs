using Microsoft.EntityFrameworkCore;

namespace Food_Waste_API
{
    class MenuDb : DbContext
    {
        public MenuDb(DbContextOptions<MenuDb> options)
        : base(options) { }

        public DbSet<Menu> Menus => Set<Menu>();
        public DbSet<Dish> Dishes => Set<Dish>();
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    }
}

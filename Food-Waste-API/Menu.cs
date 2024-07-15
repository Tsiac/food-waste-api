using Microsoft.EntityFrameworkCore;

namespace Food_Waste_API
{
    public class Menu
    {
        public int id { get; set; }
        public string dateString { get; set; }
        public List<Dish> dishes { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public bool isComplete { get; set; }
        public bool isFish { get; set; }
        public bool isMeat { get; set; }
        public bool isVeg { get; set; }

    }

    public class Dish
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string name { get; set; }
        public List<Ingredient> ingredients { get; set; }
    }

    public class Ingredient
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string name { get; set; }
        public string quantity { get; set; }
        public string counter { get; set; }
    }

}
